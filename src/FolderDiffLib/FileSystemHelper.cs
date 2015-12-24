using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using FolderDiffLib.DiffTools;

namespace FolderDiffLib
{
    public class FileSystemHelper
    {
        private readonly IFileSystem _fileSystem;

        public FileSystemHelper(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public string PathCombine(string dir, string file)
        {
            return _fileSystem.Path.Combine(dir, file);
        }
        
        public bool FileExists(string path)
        {
            return _fileSystem.FileInfo.FromFileName(path).Exists;
        }

        public MyFileInfo GetFile(string path)
        {
            var file = _fileSystem.FileInfo.FromFileName(path);
            return new MyFileInfo(file, path);
        }

        public IEnumerable<MyFileInfo> GetFiles(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
        {
            return _fileSystem.DirectoryInfo
                              .FromDirectoryName(path)
                              .GetFiles(searchPattern, searchOption)
                              .Select(x => new MyFileInfo(x, path));
        }

        public void CopyFile(FileInfoBase file, string destinationPath, bool force = false)
        {
            
        }
    }
}
