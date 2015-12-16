using System;
using System.Linq;
using Delimon.Win32.IO;
using FolderDiffLib;
using FolderDiffLib.DiffTools;
using FolderDiffLib.DiffTools.Interfaces;

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
                (file, refFiles) =>
                {
                    var referenceFile = Path.Combine(referenceFolder, file.RelativePath);
                    return _diffTool.FileExists(referenceFile) && file.File.LastWriteTime > _diffTool.GetFile(referenceFile).LastWriteTime;
                },
                (file, refFiles) =>
                {
                    var referenceFile = Path.Combine(referenceFolder, file.RelativePath);
                    return !_diffTool.FileExists(referenceFile);
                });

            var output = string.Format("\n{0}\n", string.Join("\n", files.OrderBy(x => x.FullName).Select(x => x.FullName)));

            return output;                
        }
    }
}