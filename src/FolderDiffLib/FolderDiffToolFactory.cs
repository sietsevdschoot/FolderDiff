using System.IO.Abstractions;
using FolderDiffLib.DelimonHelpers;

namespace FolderDiffLib
{
    public class FolderDiffToolFactory
    {
        public IFolderDiffTool Create(bool supportLongFilenames = false)
        {
            var fileSystem = supportLongFilenames
                ? (IFileSystem)new DelimonFileSystem()
                : (IFileSystem)new FileSystem();

            return new FolderDiffTool(fileSystem);
        }
    }
}