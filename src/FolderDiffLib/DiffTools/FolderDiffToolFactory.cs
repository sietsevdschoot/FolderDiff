using System.IO.Abstractions;
using FolderDiffLib.DelimonHelpers;
using FolderDiffLib.DiffTools.Interfaces;

namespace FolderDiffLib.DiffTools
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