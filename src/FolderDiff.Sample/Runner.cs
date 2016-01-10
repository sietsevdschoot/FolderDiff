using System.Linq;

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
                (diffFile, refFiles) =>
                {
                    var referenceFile = _fileHelper.GetFile(_fileHelper.PathCombine(referenceFolder, diffFile.RelativePath));
                    
                    var isNewer = referenceFile.File.Exists && diffFile.File.LastWriteTime > referenceFile.File.LastWriteTime;
                    var isUnique = !referenceFile.File.Exists;

                    return isNewer || isUnique;
                });

            var output = string.Format("\n{0}\n", string.Join("\n", files.OrderBy(x => x.FullName).Select(x => x.FullName)));

            return output;                
        }
    }
}