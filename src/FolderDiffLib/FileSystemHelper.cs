using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using FolderDiffLib.DiffTools;

namespace FolderDiffLib
{
    public class FileSystemHelper
    {
        private readonly IFileSystem _fileSystem;

        public FileSystemHelper(IFileSystem fileSystem)
        {
            var codebaseUri = Assembly.GetExecutingAssembly().CodeBase;
            var file = fileSystem.FileInfo.FromFileName(new Uri(codebaseUri).LocalPath);
            fileSystem.Directory.SetCurrentDirectory(file.Directory.FullName);
            
            _fileSystem = fileSystem;
        }

        public string PathCombine(params string[] paths)
        {
            return _fileSystem.Path.Combine(paths);
        }

        public MyFileInfo GetFile(string fullName, string referencePath = null)
        {
            var tmpFile = _fileSystem.FileInfo.FromFileName(fullName);

            var directoryName = !string.IsNullOrEmpty(tmpFile.DirectoryName.TrimEnd('.'))
                ? tmpFile.DirectoryName.TrimEnd('.')
                : _fileSystem.Directory.GetCurrentDirectory();

            var cleanedFullName = PathCombine(directoryName, tmpFile.Name);
            var file = _fileSystem.FileInfo.FromFileName(cleanedFullName);

            return new MyFileInfo(file, referencePath ?? file.Directory.FullName);
        }

        public IEnumerable<MyFileInfo> GetFiles(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
        {
            return _fileSystem.DirectoryInfo
                              .FromDirectoryName(path)
                              .GetFiles(searchPattern, searchOption)
                              .Select(x => GetFile(x.FullName, path));
        }
        
        public void CopyFile(MyFileInfo file, string destinationPath, bool overwrite = false, bool force = false)
        {
            var copyFile = new Action<MyFileInfo, string>((f, destinationFullPath) => f.File.CopyTo(destinationFullPath, overwrite));

            ManipulateFile(copyFile, file, destinationPath, force);
        }

        public void MoveFile(MyFileInfo file, string destinationPath, bool force = false)
        {
            var moveFile = new Action<MyFileInfo, string>((f, destinationFullPath) => f.File.MoveTo(destinationFullPath));

            ManipulateFile(moveFile, file, destinationPath, force);
        }

        public void CopyFiles(IEnumerable<MyFileInfo> filesToCopy, string destinationPath, bool overwrite = false, bool force = false)
        {
            foreach (var fileToCopy in filesToCopy)
            {
                CopyFile(fileToCopy, destinationPath, overwrite, force);
            }
        }

        public void MoveFiles(IEnumerable<MyFileInfo> filesToMove, string destinationPath, bool force = false)
        {
            foreach (var fileToMove in filesToMove)
            {
                MoveFile(fileToMove, destinationPath, force);
            }
        }

        public void MoveDirectory(string directoryFullName, string destinationFullName)
        {
            _fileSystem.DirectoryInfo.FromDirectoryName(directoryFullName).MoveTo(destinationFullName);
        }

        public void DeleteFile(string path)
        {
            _fileSystem.File.Delete(path);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            _fileSystem.Directory.Delete(path, recursive);
        }

        public void CreateDirectory(string path)
        {
            var directory = _fileSystem.DirectoryInfo.FromDirectoryName(path);

            if (!directory.Exists)
            {
                var nonExistingFolders = new Stack<DirectoryInfoBase>();
                while (!directory.Exists)
                {
                    nonExistingFolders.Push(directory);
                    directory = directory.Parent;
                }

                nonExistingFolders.ToList().ForEach(dir => dir.Create());
            }
        }

        private void ManipulateFile(Action<MyFileInfo, string> fileAction, MyFileInfo file, string destinationPath, bool force = false)
        {
            var destination = _fileSystem.DirectoryInfo.FromDirectoryName(destinationPath);

            if (!destination.Exists && !force)
                throw new IOException(string.Format("The system cannot find the path specified. {0}", destinationPath));

            var destinationFullName = _fileSystem.Path.Combine(destinationPath, file.RelativePath);
            var destinationFile = _fileSystem.FileInfo.FromFileName(destinationFullName);

            if (!destinationFile.Directory.Exists)
            {
                CreateDirectory(destinationFile.Directory.FullName);
            }

            fileAction(file, destinationFullName);
        }
    }
}
