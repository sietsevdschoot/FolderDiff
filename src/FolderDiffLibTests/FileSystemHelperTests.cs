using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using FolderDiffLib;
using FolderDiffLib.DiffTools;
using FolderDiffLibTests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FolderDiffLibTests
{
    [TestClass]
    public class FileSystemHelperTests
    {
        private MockFileSystem _fileSystem;
        private FileSystemHelper _fileSystemHelper;

        [TestInitialize]
        public void Initialize()
        {
            _fileSystem = new MockFileSystem();
            _fileSystemHelper = new FileSystemHelper(_fileSystem);
        }

        [TestMethod]
        public void GetFile_Gets_file_from_fileSystem()
        {
            // Arrange
            const string filename = @"C:\Temp\Folder\1.txt";
            _fileSystem.AddFile(filename, new MockFileData(string.Empty));

            // Act
            var actual = _fileSystemHelper.GetFile(filename);

            // Assert
            actual.Should().BeAssignableTo<MyFileInfo>();
        }

        [TestMethod]
        public void GetFiles_returns_selected_files()
        {
            // Arrange
            _fileSystem.AddFile(@"C:\Temp\Folder\1.txt", new MockFileData(string.Empty));
            _fileSystem.AddFile(@"C:\Temp\Folder\2.txt", new MockFileData(string.Empty));
            _fileSystem.AddFile(@"C:\Temp\Folder\SubFolder\3.txt", new MockFileData(string.Empty));

            // Act
            var actual = _fileSystemHelper.GetFiles(@"C:\Temp\", "*.*", SearchOption.AllDirectories);

            // Assert
            actual.Should().HaveCount(3);
        }

        [TestMethod]
        public void CopyFile_If_destination_parent_folder_does_not_exist_and_force_is_false_throws_exception()
        {
            // Arrange
            const string fileToCopy = @"c:\Temp\Folder\1.txt";
            const string destinationPath = @"c:\Destination\";
            
            _fileSystem.AddFile(fileToCopy, new MockFileData(string.Empty));
            var file = _fileSystemHelper.GetFile(fileToCopy);

            // Act
            var action = new Action(() => _fileSystemHelper.CopyFile(file, destinationPath, overwrite: true, force: false));

            // Assert
            action.ShouldThrow<IOException>();
        }

        [TestMethod]
        public void CopyFile_Copies_file_to_destination_folder()
        {
            // Arrange
            const string fileToCopy = @"c:\Temp\Folder\1.txt";
            const string destinationPath = @"c:\Temp\";
            const string expected = @"c:\Temp\1.txt";

            _fileSystem.AddFile(fileToCopy, new MockFileData(string.Empty));
            var file = _fileSystemHelper.GetFile(fileToCopy, @"c:\Temp\Folder\");

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
            const string fileToCopy = @"c:\Temp\Folder\1.txt";
            const string destinationPath = @"c:\Temp\Folder\1.txt";

            _fileSystem.AddFile(fileToCopy, new MockFileData(string.Empty));
            var file = _fileSystemHelper.GetFile(fileToCopy);

            // Act
            var action = new Action(() => _fileSystemHelper.CopyFile(file, destinationPath, overwrite: false));

            // Assert
            action.ShouldThrow<IOException>();
        }

        [TestMethod]
        public void CopyFiles_Copies_files_to_destinationFolder()
        {
            // Arrange
            var filesSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\Temp\Folder\1.txt", new MockFileData(string.Empty)},                                                    
                {@"c:\Temp\Folder\2.txt", new MockFileData(string.Empty)},                                                    
                {@"c:\Temp\Folder\SubFolder\3.txt", new MockFileData(string.Empty)},                                                    
                {@"c:\Temp\Folder\SubFolder\4.txt", new MockFileData(string.Empty)},                                                    

                {@"d:\Destination\", new MockDirectoryData()},                                                    
            });            

            var fileSystemHelper = new FileSystemHelper(filesSystem);
            
            // Act
            var filesToCopy = fileSystemHelper.GetFiles(@"c:\Temp\", "*.*"); 
            fileSystemHelper.CopyFiles(filesToCopy, @"D:\Destination\", force: false);
            
            // Assert
            filesSystem.AllFiles.Contains(new List<string>
            {
                @"d:\Destination\Folder\1.txt",
                @"d:\Destination\Folder\2.txt",
                @"d:\Destination\Folder\SubFolder\3.txt",
                @"d:\Destination\Folder\SubFolder\4.txt",
            }, StringComparer.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void CopyFiles_if_force_is_true_and_destinationPath_does_not_exist_creates_destinationFolder()
        {
            // Arrange
            var filesSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"c:\Temp\Folder\1.txt", new MockFileData(string.Empty)},                                                    
                {@"c:\Temp\Folder\2.txt", new MockFileData(string.Empty)},                                                    
                {@"c:\Temp\Folder\SubFolder\3.txt", new MockFileData(string.Empty)},                                                    
                {@"c:\Temp\Folder\SubFolder\4.txt", new MockFileData(string.Empty)},                                                    
            });            

            var fileSystemHelper = new FileSystemHelper(filesSystem);
            
            // Act
            var filesToCopy = fileSystemHelper.GetFiles(@"c:\Temp\", "*.*"); 
            fileSystemHelper.CopyFiles(filesToCopy, @"C:\Destination\Documents\", force: true);
            
            // Assert
            filesSystem.AllFiles.Contains(new List<string>
            {
                @"c:\Destination\Documents\Folder\1.txt",
                @"c:\Destination\Documents\Folder\2.txt",
                @"c:\Destination\Documents\Folder\SubFolder\3.txt",
                @"c:\Destination\Documents\Folder\SubFolder\4.txt",
            }, StringComparer.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void MoveFile_Moves_file_to_destination_folder()
        {
            // Arrange
            const string fileToCopy = @"c:\Temp\1.txt";
            const string destinationPath = @"c:\Temp\Folder\";
            const string expected = @"c:\Temp\Folder\1.txt";

            _fileSystem.AddFile(fileToCopy, new MockFileData(string.Empty));
            _fileSystem.AddFile(destinationPath, new MockDirectoryData());
            var file = _fileSystemHelper.GetFile(fileToCopy);

            // Act
            _fileSystemHelper.MoveFile(file, destinationPath);

            // Assert
            _fileSystem.AllFiles.Should().HaveCount(1);
            _fileSystem.FileInfo.FromFileName(expected)
                                .Exists.Should().BeTrue();
        }

        [TestMethod]
        public void MoveFile_if_force_is_true_and_destinationPath_does_not_exist_creates_destinationFolder()
        {
            // Arrange
            const string fileToCopy = @"c:\Temp\Folder\1.txt";
            const string destinationPath = @"D:\Destination\";
            const string expected = @"D:\Destination\Folder\1.txt";

            _fileSystem.AddFile(fileToCopy, new MockFileData(string.Empty));
            _fileSystem.AddFile(destinationPath, new MockDirectoryData());
            var file = _fileSystemHelper.GetFile(fileToCopy, @"C:\Temp\");

            // Act
            _fileSystemHelper.MoveFile(file, destinationPath, force: true);

            // Assert
            _fileSystem.AllFiles.Should().HaveCount(1);
            _fileSystem.FileInfo.FromFileName(expected)
                .Exists.Should().BeTrue();
        }

        [TestMethod]
        public void MoveDirectory_moves_directory()
        {
            // Arrange
            const string directory = @"D:\Destination\";
            const string expectedDirectory = @"D:\RenamedFolder\";

            _fileSystem.AddDirectory(directory);
            _fileSystem.AddFile(directory + "1.txt", new MockFileData(string.Empty));
            _fileSystem.AddFile(directory + "2.txt", new MockFileData(string.Empty));

            // Act
            _fileSystemHelper.MoveDirectory(directoryFullName: directory, destinationFullName: expectedDirectory);

            // Assert
            _fileSystem.DirectoryInfo.FromDirectoryName(expectedDirectory)
                .Exists.Should().BeTrue();

            _fileSystem.AllFiles.Should().HaveCount(2);
        }

        [TestMethod]
        public void DeleteFile_Removes_File()
        {
            // Arrange
            const string file = @"c:\Temp\Folder\1.txt";

            _fileSystem.AddFile(file, new MockFileData(string.Empty));

            // Act
            _fileSystemHelper.DeleteFile(path: file);

            // Assert
            _fileSystem.AllFiles.Should().BeEmpty();
        }

        [TestMethod]
        public void DeleteDirectory_Removes_Directory()
        {
            // Arrange
            const string directory = @"c:\Temp\Folder\";

            _fileSystem.AddDirectory(directory);

            // Act
            _fileSystemHelper.DeleteDirectory(path: directory, recursive: true);

            // Assert
            _fileSystem.DirectoryInfo.FromDirectoryName(directory).Exists.Should().BeFalse();
            _fileSystem.DirectoryInfo.FromDirectoryName(@"c:\Temp\").Exists.Should().BeTrue();
        }

        [TestMethod]
        public void CombinePath_Returns_combined_paths()
        {
            _fileSystemHelper.PathCombine(@"c:\", @"Temp\Folder", @"1.txt")
                             .Should().BeEquivalentTo(@"C:\Temp\Folder\1.txt");
        }
    }
}