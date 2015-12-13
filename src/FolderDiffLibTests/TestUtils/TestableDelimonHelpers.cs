using System.IO.Abstractions;
using System.Linq;
using Delimon.Win32.IO;
using FolderDiffLib.DelimonHelpers.Util;

namespace FolderDiffLibTests.TestUtils
{
    public class TestableDelimonHelpers : DelimonHelpersBase
    {
        private readonly IFileSystem _fileSystem;

        public TestableDelimonHelpers(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public override DirectoryInfo[] FindDirectoriesInfos(string source, string searchPattern)
        {
            return _fileSystem.DirectoryInfo.FromDirectoryName(source).GetDirectories(searchPattern)
                              .Select(x => new DirectoryInfo(x.FullName))
                              .ToArray();
        }

        public override FileInfo[] FindFilesInfos(string source, string searchPattern)
        {
            return _fileSystem.DirectoryInfo.FromDirectoryName(source).GetFiles(searchPattern)
                              .Select(x => new FileInfo(x.FullName))
                              .ToArray();
        }
    }
}