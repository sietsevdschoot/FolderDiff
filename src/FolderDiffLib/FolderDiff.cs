using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using FolderDiffLib.DiffTools;

namespace FolderDiffLib
{
    public class FolderDiff
    {
        public List<FileInfoBase> DiffFolder(
            string referenceFolder,
            string differenceFolder,
            bool supportLongFilenames,
            params Func<MyFileInfo, IEnumerable<MyFileInfo>, bool>[] fileSelectors)
        {
            var factory = new FolderDiffToolFactory();
            var diffTool = factory.Create(supportLongFilenames);

            return diffTool.DiffFolder(referenceFolder, differenceFolder, fileSelectors);
        }
    }
}