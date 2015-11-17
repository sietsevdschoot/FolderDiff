using Delimon.Win32.IO;

namespace FolderDiffLib.DelimonHelpers.Util
{
    public abstract class DelimonHelpersBase
    {
        public abstract DirectoryInfo[] FindDirectoriesInfos(string source, string searchPattern);

        public abstract FileInfo[] FindFilesInfos(string source, string searchPattern);
    }
}