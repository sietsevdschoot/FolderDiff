using System.IO.Abstractions;


namespace FolderDiffLib.DiffTools
{
    public class FolderDiffFactory
    {
        public static FolderDiff Create(bool supportLongFilenames)
        {
            var fileSystem = FileSystemFactory.Create(supportLongFilenames);
            return Create(fileSystem);
        }

        public static FolderDiff Create(IFileSystem fileSystem)
        {
            var fileSystemHelper = new FileSystemHelper(fileSystem);
            return new FolderDiff(fileSystem, fileSystemHelper);
        }
    }
}