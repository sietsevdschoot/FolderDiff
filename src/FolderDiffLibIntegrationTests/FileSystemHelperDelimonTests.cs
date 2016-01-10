using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using FluentAssertions;
using FolderDiffLib;
using FolderDiffLib.DelimonHelpers;
using FolderDiffLib.DiffTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FolderDiffLibIntegrationTests
{
    [TestClass]
    public class FileSystemHelperDelimonTests
    {
        private DelimonFileSystem _fileSystem;
        private FileSystemHelper _fileSystemHelper;
        private static ConcurrentDictionary<int, string> _testFolderLookup;
        
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _testFolderLookup = new ConcurrentDictionary<int, string>();
        }

        [TestInitialize]
        public void Initialize()
        {
            _fileSystem = new DelimonFileSystem();
            _fileSystemHelper = new FileSystemHelper(_fileSystem);

            _testFolderLookup[Thread.CurrentThread.ManagedThreadId] = Guid.NewGuid().ToString("N");
        }
        
        [TestMethod]
        public void GetFile_Gets_file_from_fileSystem()
        {
            // Arrange
            var filename = GetRealPath(@"C:\Temp\Folder\1.txt");
            
            CreateFile(filename);

            // Act
            var actual = _fileSystemHelper.GetFile(filename);

            // Assert
            actual.Should().BeAssignableTo<MyFileInfo>();
        }

        [TestMethod]
        public void GetFiles_returns_selected_files()
        {
            // Arrange
            CreateFile(GetRealPath(@"C:\Temp\Folder\1.txt"));
            CreateFile(GetRealPath(@"C:\Temp\Folder\2.txt"));
            CreateFile(GetRealPath(@"C:\Temp\Folder\SubFolder\3.txt"));

            // Act
            var actual = _fileSystemHelper.GetFiles(GetRealPath(@"C:\Temp\"), "*.*", SearchOption.AllDirectories);

            // Assert
            actual.Should().HaveCount(3);
        }

        [TestMethod]
        public void CopyFile_If_destination_parent_folder_does_not_exist_and_force_is_false_throws_exception()
        {
            // Arrange
            const string fileToCopy = @"c:\Temp\Folder\1.txt";

            CreateFile(GetRealPath(fileToCopy));

            var file = _fileSystemHelper.GetFile(fileToCopy);

            // Act
            var action = new Action(() => _fileSystemHelper.CopyFile(file, GetRealPath(@"c:\Destination\"), overwrite: true, force: false));

            // Assert
            action.ShouldThrow<IOException>();
        }

        [TestMethod]
        public void CopyFile_Copies_file_to_destination_folder()
        {
            // Arrange
            var fileToCopy = GetRealPath(@"c:\Temp\Folder\1.txt");
            var destinationPath = GetRealPath(@"c:\Temp\");
            var expected = GetRealPath(@"c:\Temp\1.txt");

            CreateFile(fileToCopy);
            CreateDirectory(destinationPath);

            var file = _fileSystemHelper.GetFile(fileToCopy, GetRealPath(@"c:\Temp\Folder\"));

            // Act
            _fileSystemHelper.CopyFile(file, destinationPath, overwrite: true);

            // Assert
            _fileSystem.FileInfo.FromFileName(expected)
                                .Exists.Should().BeTrue();
        }

        [TestMethod]
        public void CopyFile_If_destination_file_exists_and_overwrite_is_false_throws_exception()
        {
            // Arrange
            var fileToCopy = GetRealPath(@"c:\Temp\Folder\1.txt");
            var destinationPath = GetRealPath(@"c:\Temp\Folder\");

            CreateFile(fileToCopy);

            var file = _fileSystemHelper.GetFile(fileToCopy);

            // Act
            var action = new Action(() => _fileSystemHelper.CopyFile(file, destinationPath, overwrite: false));

            // Assert
            action.ShouldThrow<Exception>()
                  .WithMessage("*file Exists*");
        }

        [TestMethod]
        public void CopyFiles_Copies_files_to_destinationFolder()
        {
            // Arrange
            CreateFile(GetRealPath(@"c:\Temp\Folder\1.txt"));
            CreateFile(GetRealPath(@"c:\Temp\Folder\2.txt"));
            CreateFile(GetRealPath(@"c:\Temp\Folder\SubFolder\3.txt"));
            CreateFile(GetRealPath(@"c:\Temp\Folder\SubFolder\4.txt"));

            CreateDirectory(GetRealPath(@"d:\Destination\"));

            var fileSystemHelper = new FileSystemHelper(_fileSystem);

            // Act
            var filesToCopy = fileSystemHelper.GetFiles(GetRealPath(@"c:\Temp\"), "*.*");
            fileSystemHelper.CopyFiles(filesToCopy, GetRealPath(@"D:\Destination\"), force: false);

            // Assert
            new List<string>
            {
                @"d:\Destination\Folder\1.txt",
                @"d:\Destination\Folder\2.txt",
                @"d:\Destination\Folder\SubFolder\3.txt",
                @"d:\Destination\Folder\SubFolder\4.txt",
            }
            .ForEach(FileShouldExist);
        }

        [TestMethod]
        public void CopyFiles_if_force_is_true_and_destinationPath_does_not_exist_creates_destinationFolder()
        {
            // Arrange
            CreateFile(GetRealPath(@"c:\Temp\Folder\1.txt"));
            CreateFile(GetRealPath(@"c:\Temp\Folder\2.txt"));
            CreateFile(GetRealPath(@"c:\Temp\Folder\SubFolder\3.txt"));
            CreateFile(GetRealPath(@"c:\Temp\Folder\SubFolder\4.txt"));

            var fileSystemHelper = new FileSystemHelper(_fileSystem);

            // Act
            var filesToCopy = fileSystemHelper.GetFiles(GetRealPath(@"c:\Temp\"), "*.*");
            fileSystemHelper.CopyFiles(filesToCopy, GetRealPath(@"D:\Destination\"), force: true);

            // Assert
            new List<string>
            {
                @"d:\Destination\Folder\1.txt",
                @"d:\Destination\Folder\2.txt",
                @"d:\Destination\Folder\SubFolder\3.txt",
                @"d:\Destination\Folder\SubFolder\4.txt",
            }
            .ForEach(FileShouldExist);
        }

        [TestMethod]
        public void MoveFile_Moves_file_to_destination_folder()
        {
            // Arrange
            var fileToCopy = GetRealPath(@"c:\Temp\1.txt");
            var destinationPath = GetRealPath(@"c:\Temp\Folder\");
            var expected = GetRealPath(@"c:\Temp\Folder\1.txt");

            CreateFile(fileToCopy);
            CreateDirectory(destinationPath);

            var file = _fileSystemHelper.GetFile(fileToCopy);

            // Act
            _fileSystemHelper.MoveFile(file, destinationPath);

            // Assert
            _fileSystem.FileInfo.FromFileName(fileToCopy).Exists.Should().BeFalse();
            _fileSystem.FileInfo.FromFileName(expected).Exists.Should().BeTrue();
        }

        [TestMethod]
        public void MoveFile_if_force_is_true_and_destinationPath_does_not_exist_creates_destinationFolder()
        {
            // Arrange
            var fileToCopy = GetRealPath(@"c:\Temp\Folder\1.txt");
            var destinationPath = GetRealPath(@"D:\Destination\");
            var expected = GetRealPath(@"D:\Destination\Folder\1.txt");

            CreateFile(fileToCopy);
            CreateDirectory(destinationPath);

            var file = _fileSystemHelper.GetFile(fileToCopy, GetRealPath(@"C:\Temp\"));

            // Act
            _fileSystemHelper.MoveFile(file, destinationPath, force: true);

            // Assert
            _fileSystem.FileInfo.FromFileName(fileToCopy).Exists.Should().BeFalse();
            _fileSystem.FileInfo.FromFileName(expected).Exists.Should().BeTrue();
        }

        [TestMethod]
        public void MoveDirectory_moves_directory()
        {
            // Arrange
           var directory = GetRealPath(@"D:\Destination\");
           var expectedDirectory = GetRealPath(@"D:\RenamedFolder\");

            CreateFile(GetRealPath(@"D:\Destination\1.txt"));
            CreateFile(GetRealPath(@"D:\Destination\2.txt"));
            
            // Act
            _fileSystemHelper.MoveDirectory(directoryFullName: directory, destinationFullName: expectedDirectory);

            // Assert
            _fileSystem.DirectoryInfo.FromDirectoryName(expectedDirectory)
                       .Exists.Should().BeTrue();

            _fileSystemHelper.GetFiles(expectedDirectory, "*.*").Should().HaveCount(2);
        }

        [TestMethod]
        public void DeleteFile_Removes_File()
        {
            // Arrange
            var file = GetRealPath(@"c:\Temp\Folder\1.txt");

            CreateFile(file);

            // Act
            _fileSystemHelper.DeleteFile(path: file);

            // Assert
            _fileSystem.FileInfo.FromFileName(file).Exists.Should().BeFalse();
        }

        [TestMethod]
        public void DeleteDirectory_Removes_Directory()
        {
            // Arrange
            var directory = GetRealPath(@"c:\Temp\Folder\");

            CreateDirectory(directory);

            // Act
            _fileSystemHelper.DeleteDirectory(path: directory, recursive: true);

            // Assert
            _fileSystem.DirectoryInfo.FromDirectoryName(directory).Exists.Should().BeFalse();
            _fileSystem.DirectoryInfo.FromDirectoryName(GetRealPath(@"c:\Temp\")).Exists.Should().BeTrue();
        }

        [TestMethod]
        public void CombinePath_Returns_combined_paths()
        {
            _fileSystemHelper.PathCombine(@"c:\", @"Temp\Folder", @"1.txt")
                             .Should().BeEquivalentTo(@"C:\Temp\Folder\1.txt");
        }

        private void CreateFile(string path)
        {
            var fileInfo = _fileSystem.FileInfo.FromFileName(path);

            if (!fileInfo.Directory.Exists)
            {
                CreateDirectory(fileInfo.Directory.FullName);
            }

            _fileSystem.File.WriteAllText(fileInfo.FullName, string.Empty);
        }

        private void CreateDirectory(string path)
        {
            var directoryInfo = _fileSystem.DirectoryInfo.FromDirectoryName(path);

            if (!directoryInfo.Exists)
            {
                _fileSystemHelper.CreateDirectory(directoryInfo.FullName);
            }
        }

        private string GetRealPath(string fullFilePath)
        {
            var tempPath = _fileSystem.Path.GetTempPath();
            var basePath = _testFolderLookup[Thread.CurrentThread.ManagedThreadId];

            var fileInfo = _fileSystem.FileInfo.FromFileName(fullFilePath);

            var folder = fileInfo.Directory.FullName.Replace(fileInfo.Directory.Root.FullName, string.Empty);

            var destinationFullName = fullFilePath.EndsWith(@"\")
                ? _fileSystem.Path.Combine(tempPath, "FolderDiff", basePath, folder)
                : _fileSystem.Path.Combine(tempPath, "FolderDiff", basePath, folder, fileInfo.Name);

            return destinationFullName;
        }

        private void FileShouldExist(string fileFullname)
        {
            _fileSystemHelper.GetFile(GetRealPath(fileFullname)).File.Exists.Should().BeTrue(fileFullname);
        }
    }
}