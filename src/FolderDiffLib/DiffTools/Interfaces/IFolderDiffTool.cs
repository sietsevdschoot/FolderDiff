using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace FolderDiffLib.DiffTools.Interfaces
{
    public interface IFolderDiffTool
    {
        List<FileInfoBase> DiffFolder(
            string referenceFolder,
            string differenceFolder,
            params Func<MyFileInfo, IEnumerable<MyFileInfo>, bool>[] fileSelectors);

        bool FileExists(string path);
        FileInfoBase GetFile(string path);
    }
}