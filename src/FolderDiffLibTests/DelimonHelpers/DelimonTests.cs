using System.IO;
using Delimon.Win32.IO;
using FluentAssertions;
using FolderDiffLib;
using FolderDiffLib.DelimonHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FolderDiffLibTests.DelimonHelpers
{
    [TestClass]
    public class DelimonTests
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void MethodName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var fileSystem = new DelimonFileSystem();

            var helper = new FileSystemHelper(fileSystem);

            fileSystem.Directory.SetCurrentDirectory(@"D:\Development\Github\FolderDiff\src\FolderDiffPowerShell");

            // Act
            var file = helper.GetFile(@".\Measure.ps1");

            // Assert
            file.File.Exists.Should().BeTrue();
        }
    }
}