using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using FolderDiffLib;
using FolderDiffLib.DiffTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FolderDiffLibTests
{
    [TestClass]
    public class FolderDiffTests
    {
        private MockFileSystem _fileSystem;
        private FolderDiff _folderDiff;

        [TestInitialize]
        public void Initailize()
        {
            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\Temp\1.txt", new MockFileData(string.Empty)},
                {@"c:\Temp\2.txt", new MockFileData(string.Empty)},
                {@"c:\Temp\3.txt", new MockFileData(string.Empty)},
            });

            _folderDiff = FolderDiffFactory.Create(_fileSystem);
        }

        [TestMethod]
        public void DiffFolder_returns_a_distint_set_of_selected_files()
        {
            // Act
            var actual = _folderDiff.DiffFolder(
                @"c:\Temp\",
                @"c:\Temp\",
                (diffFile, referenceFiles) => true,
                (diffFile, referenceFiles) => true);

            // Assert
            actual.Should().HaveCount(3);
        }

        [TestMethod]
        public void DiffFolder_If_differenceFolder_IsNullOrEmpty_returns_all_files_from_referenceFolder()
        {
            // Act
            var actual = _folderDiff.DiffFolder(
                @"c:\Temp\",
                @"c:\Temp\",
                (diffFile, referenceFiles) => true);

            // Assert
            actual.Should().HaveCount(3);
        }
    }
}