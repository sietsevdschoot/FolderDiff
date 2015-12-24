using System.IO.Abstractions;
using FolderDiffLib.DelimonHelpers;

namespace FolderDiffLib.DiffTools
{
    public class FileSystemFactory
    {
        public static IFileSystem Create(bool supportLongFilenames)
        {
            return supportLongFilenames
                ? (IFileSystem)new DelimonFileSystem()
                : (IFileSystem)new FileSystem();
       }
       
       


       
    }
}