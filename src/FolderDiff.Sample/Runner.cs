using System.Linq;
using Delimon.Win32.IO;

namespace FolderDiffLib.Sample
{
    public class Runner
    {
        private readonly FolderDiff _diffTool;
        private readonly FileSystemHelper _fileHelper;

        public Runner(FolderDiff diffTool, FileSystemHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _diffTool = diffTool;
        }

        public string GetNewerAndUniqueDiffFiles(string referenceFolder, string differenceFolder)
        {
            var files = _diffTool.DiffFolder(referenceFolder, differenceFolder,
                (file, refFiles) =>
                {
                    var referenceFile = Path.Combine(referenceFolder, file.RelativePath);
                    return _fileHelper.FileExists(referenceFile) && file.File.LastWriteTime > _fileHelper.GetFile(referenceFile).File.LastWriteTime;
                },
                (file, refFiles) =>
                {
                    var referenceFile = Path.Combine(referenceFolder, file.RelativePath);
                    return !_fileHelper.FileExists(referenceFile);
                });

            var output = string.Format("\n{0}\n", string.Join("\n", files.OrderBy(x => x.FullName).Select(x => x.FullName)));

            return output;                
        }
    }
}