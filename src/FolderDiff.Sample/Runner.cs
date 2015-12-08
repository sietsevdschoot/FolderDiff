using System;
using System.Linq;
using FolderDiffLib;

namespace FolderDiff.Sample
{
    public class Runner
    {
        private readonly IFolderDiffTool _diffTool;

        public Runner(IFolderDiffTool diffTool)
        {
            _diffTool = diffTool;
        }

        public string GetNewerAndUniqueDiffFiles(string referenceFolder, string differenceFolder)
        {
            var files = _diffTool.DiffFolder(referenceFolder, differenceFolder,
                (file, refFiles) => refFiles.Any(refFile => file.RelativePath.Equals(refFile.RelativePath) && file.File.LastWriteTime > refFile.File.LastWriteTime),
                (file, refFiles) => !refFiles.Select(x => x.RelativePath).Contains(file.RelativePath)
            );

            var output = string.Format("\n{0}\n", string.Join("\n", files.OrderBy(x => x.FullName).Select(x => x.FullName)));

            return output;                
        }
    }
}