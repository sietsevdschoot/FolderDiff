using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace FolderDiffLib
{
    public interface IFolderDiffTool
    {
        List<FileInfoBase> DiffFolder(
            string referenceFolder,
            string differenceFolder,
            params Func<MyFileInfo, IEnumerable<MyFileInfo>, bool>[] fileSelectors);
    }
}