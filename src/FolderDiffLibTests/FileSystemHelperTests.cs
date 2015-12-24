using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using FolderDiffLib;
using FolderDiffLib.DiffTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FolderDiffLibTests
{
    [TestClass]
    public class FileSystemHelperTests
    {
        private MockFileSystem _fileSystem;
        private FileSystemHelper _fileSystemHelper;

        [TestInitialize]
        public void Initailize()
        {
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\Temp\Folder\1.txt", new MockFileData(string.Empty)},
                {@"c:\Temp\Folder\2.txt", new MockFileData(string.Empty)},
                {@"c:\Temp\Folder\3.txt", new MockFileData(string.Empty)},
                {@"c:\Temp\Folder\SubFolder\4.txt", new MockFileData(string.Empty)},
                {@"c:\Temp\Folder\SubFolder\5.txt", new MockFileData(string.Empty)},
                {@"c:\Temp\Folder\photo.jpeg", new MockFileData(string.Empty)},
            });

            _fileSystemHelper = new FileSystemHelper(_fileSystem);
        }

        [TestMethod]
        public void FileExists_if_file_exist_returns_true()
        {
            _fileSystemHelper.FileExists(@"C:\Temp\Folder\1.txt").Should().BeTrue();
        }

        [TestMethod]
        public void GetFile_Gets_file_from_fileSystem()
        {
            _fileSystemHelper.GetFile(@"c:\Temp\Folder\3.txt").Should().BeAssignableTo<MyFileInfo>();
        }

        [TestMethod]
        public void GetFiles_returns_selected_files()
        {
            _fileSystemHelper.GetFiles(@"c:\Temp\", "*.txt", SearchOption.AllDirectories)
                             .Should().HaveCount(5);
        }


    }
}