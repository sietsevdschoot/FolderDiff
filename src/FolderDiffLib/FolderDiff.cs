using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using FolderDiffLib.DelimonHelpers.Util;
using FolderDiffLib.DiffTools;

namespace FolderDiffLib
{
    public class FolderDiff
    {
        private readonly IFileSystem _fileSystem;
        private readonly FileSystemHelper _fileHelper;

        public FolderDiff(IFileSystem fileSystem, FileSystemHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _fileSystem = fileSystem;
        }

        public List<FileInfoBase> DiffFolder(
            string referenceFolder,
            string differenceFolder,
            params Func<MyFileInfo, IEnumerable<MyFileInfo>, bool>[] fileSelectors)
        {
            var referencePath = _fileSystem.DirectoryInfo.FromDirectoryName(referenceFolder).FullName;
            var differencePath = _fileSystem.DirectoryInfo.FromDirectoryName(differenceFolder).FullName;

            var referenceFiles = _fileHelper.GetFiles(referencePath, "*.*").ToList();
            var differenceFiles = _fileHelper.GetFiles(differencePath, "*.*").ToList();

            var selectedFiles = fileSelectors.SelectMany(fileSelector => differenceFiles.Where(file => fileSelector(file, referenceFiles)));

            var uniqueFiles = selectedFiles
                .Select(x => x.File)
                .Distinct(x => x.FullName);

            return uniqueFiles.ToList();
        }
    }
}