using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using FolderDiffLib.DelimonHelpers.Util;
using FolderDiffLib.DiffTools.Interfaces;

namespace FolderDiffLib.DiffTools
{
    public class FolderDiffTool : IFolderDiffTool
    {
        private readonly IFileSystem _fileSystem;

        public FolderDiffTool(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public List<FileInfoBase> DiffFolder(
            string referenceFolder,
            string differenceFolder,
            params Func<MyFileInfo, IEnumerable<MyFileInfo>, bool>[] fileSelectors)
        {
            var referencePath = _fileSystem.DirectoryInfo.FromDirectoryName(referenceFolder).FullName;
            var differencePath = _fileSystem.DirectoryInfo.FromDirectoryName(differenceFolder).FullName;

            var referenceFiles = GetFiles(referencePath).ToList();
            var differenceFiles = GetFiles(differencePath).ToList();

            var selectedFiles = fileSelectors.SelectMany(fileSelector => differenceFiles.Where(file => fileSelector(file, referenceFiles)));

            var uniqueFiles = selectedFiles
                .Select(x => x.File)
                .Distinct(x => x.FullName);

            return uniqueFiles.ToList();
        }

        public bool FileExists(string path)
        {
            return _fileSystem.FileInfo.FromFileName(path).Exists;
        }

        public FileInfoBase GetFile(string path)
        {
            return _fileSystem.FileInfo.FromFileName(path);
        }

        private IEnumerable<MyFileInfo> GetFiles(string path)
        {
            return _fileSystem.DirectoryInfo.FromDirectoryName(path)
                              .GetFiles("*.*", SearchOption.AllDirectories)
                              .Select(f => new MyFileInfo(f, path));
        }
    }
}