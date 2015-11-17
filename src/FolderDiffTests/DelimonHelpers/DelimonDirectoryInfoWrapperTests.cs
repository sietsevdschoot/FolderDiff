using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using FluentAssertions;
using FolderDiffLib.DelimonHelpers;
using FolderDiffLib.DelimonHelpers.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FolderDiffTests.DelimonHelpers
{
    [TestClass]
    public class DelimonDirectoryInfoWrapperTests
    {
        private MockFileSystem _fileSystem;
        private DelimonDirectoryInfoWrapper _directory;

        [TestInitialize]
        public void Initialise()
        {
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\temp\1.txt", new MockFileData(string.Empty) },
                { @"c:\temp\2.txt", new MockFileData(string.Empty) },
                { @"c:\temp\3.txt", new MockFileData(string.Empty) },
                { @"c:\temp\SubFolder\4.txt", new MockFileData(string.Empty) },
                { @"c:\temp\SubFolder\Sub\5.txt", new MockFileData(string.Empty) },
                { @"c:\temp\SubFolder\Sub\6.txt", new MockFileData(string.Empty) },
            });

            var helpers = new TestableDelimonHelpers(_fileSystem);

            var recursionHelpers = new MyRecursionHelpers(helpers);
            _directory = new DelimonDirectoryInfoWrapper(recursionHelpers, new Delimon.Win32.IO.DirectoryInfo(@"c:\temp"));
        }

        [TestMethod]
        public void GetFiles_gets_files_recursively()
        {
            var files = _directory.GetFiles("*.*", SearchOption.AllDirectories);
            
            files.Should().HaveCount(6);
        }

        [TestMethod]
        public void GetFiles_gets_directories_recursively()
        {
            var files = _directory.GetDirectories("*", SearchOption.AllDirectories);
            
            files.Should().HaveCount(2);
        }
    }
}