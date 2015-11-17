using Delimon.Win32.IO;

namespace FolderDiffLib.DelimonHelpers.Util
{
    public class DelimonHelpersWrapper : DelimonHelpersBase
    {
        public override DirectoryInfo[] FindDirectoriesInfos(string source, string searchPattern)
        {
            return Helpers.FindDirectoriesInfos(source, searchPattern);
        }

        public override FileInfo[] FindFilesInfos(string source, string searchPattern)
        {
            return Helpers.FindFilesInfos(source, searchPattern);
        }
    }
}