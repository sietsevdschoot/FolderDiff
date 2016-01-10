using System.IO;
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

        public override Delimon.Win32.IO.DirectoryInfo[] FindDirectoriesInfos(string source, string searchPattern)
        {
            return _fileSystem.DirectoryInfo.FromDirectoryName(source).GetDirectories(searchPattern)
                              .Select(x => new Delimon.Win32.IO.DirectoryInfo(x.FullName))
                              .ToArray();
        }

        public override Delimon.Win32.IO.FileInfo[] FindFilesInfos(string source, string searchPattern)
        {
            return _fileSystem.DirectoryInfo.FromDirectoryName(source).GetFiles(searchPattern)
                              .Select(x => new Delimon.Win32.IO.FileInfo(x.FullName))
                              .ToArray();
        }
    }
}