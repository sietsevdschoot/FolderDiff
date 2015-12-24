using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Security.AccessControl;
using Delimon.Win32.IO;
using FolderDiffLib.DelimonHelpers.Util;
using SearchOption = System.IO.SearchOption;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    public class DelimonDirectoryInfoWrapper : DirectoryInfoBase
    {
        private readonly Delimon.Win32.IO.DirectoryInfo _instance;
        private readonly MyRecursionHelpers _helpers;

        public DelimonDirectoryInfoWrapper(Delimon.Win32.IO.DirectoryInfo instance)
            : this(new MyRecursionHelpers(new DelimonHelpersWrapper()), instance)
        {
        }

        public DelimonDirectoryInfoWrapper(MyRecursionHelpers helpers, DirectoryInfo instance)
        {
            _helpers = helpers;
            _instance = instance;
        }

        public DelimonDirectoryInfoWrapper(System.IO.DirectoryInfo instance)
        {
            _instance = new Delimon.Win32.IO.DirectoryInfo(instance.FullName);
        }

        public DelimonDirectoryInfoWrapper(string fullName)
        {
            _instance = new Delimon.Win32.IO.DirectoryInfo(fullName);
        }

        public override System.IO.FileAttributes Attributes
        {
            get { return MyConverters.ConvertEnum<Delimon.Win32.IO.FileAttributes, System.IO.FileAttributes>(_instance.Attributes); }
            set { _instance.Attributes = MyConverters.ConvertEnum<System.IO.FileAttributes, Delimon.Win32.IO.FileAttributes>(value); }
        }

        public override DateTime CreationTime
        {
            get { return _instance.CreationTime; }
            set { _instance.CreationTime = value; }
        }

        public override DateTime CreationTimeUtc
        {
            get { return _instance.CreationTimeUtc; }
            set { _instance.CreationTimeUtc = value; }
        }

        public override bool Exists
        {
            get { return _instance.Exists; }
        }

        public override string Extension
        {
            get { return Helpers.GetNormalPath(Delimon.Win32.IO.Path.GetExtension(_instance.FullName)); }
        }

        public override string FullName
        {
            get { return _instance.FullName; }
        }

        public override DateTime LastAccessTime
        {
            get { return _instance.LastAccessTime; }
            set { _instance.LastAccessTime = value; }
        }

        public override DateTime LastAccessTimeUtc
        {
            get { return _instance.LastAccessTimeUtc; }
            set { _instance.LastAccessTimeUtc = value; }
        }

        public override DateTime LastWriteTime
        {
            get { return _instance.LastWriteTime; }
            set { _instance.LastWriteTime = value; }
        }

        public override DateTime LastWriteTimeUtc
        {
            get { return _instance.LastWriteTimeUtc; }
            set { _instance.LastWriteTimeUtc = value; }
        }

        public override string Name
        {
            get { return _instance.Name; }
        }

        public override DirectoryInfoBase Parent
        {
            get { return new DelimonDirectoryInfoWrapper(_instance.Parent); }
        }

        public override DirectoryInfoBase Root
        {
            get { return new DelimonDirectoryInfoWrapper(_instance.Root); }
        }

        public override void Delete()
        {
            _instance.Delete();
        }

        public override void Refresh()
        {
            _instance.Refresh();
        }

        public override void Create()
        {
            _instance.Create();
        }

        public override void Create(DirectorySecurity directorySecurity)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.DirectoryInfo");
        }

        public override DirectoryInfoBase CreateSubdirectory(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.DirectoryInfo");
        }

        public override DirectoryInfoBase CreateSubdirectory(string path, DirectorySecurity directorySecurity)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.DirectoryInfo");
        }

        public override void Delete(bool recursive)
        {
            _instance.Delete(recursive);
        }

        public override IEnumerable<DirectoryInfoBase> EnumerateDirectories()
        {
            return Helpers.FindDirectoriesInfos(_instance.FullName, "*.*").WrapDirectories();
        }

        public override IEnumerable<DirectoryInfoBase> EnumerateDirectories(string searchPattern)
        {
            return Helpers.FindDirectoriesInfos(_instance.FullName, searchPattern).WrapDirectories();
        }

        public override IEnumerable<DirectoryInfoBase> EnumerateDirectories(string searchPattern, SearchOption searchOption)
        {
            return searchOption == SearchOption.TopDirectoryOnly
                ? Helpers.FindDirectoriesInfos(_instance.FullName, searchPattern).WrapDirectories()
                : _helpers.FindDirectoriesInfosRecursive(_instance.FullName, searchPattern).WrapDirectories();
        }

        public override IEnumerable<FileInfoBase> EnumerateFiles()
        {
            return Helpers.FindFilesInfos(_instance.FullName, "*.*").WrapFiles();
        }

        public override IEnumerable<FileInfoBase> EnumerateFiles(string searchPattern)
        {
            return Helpers.FindFilesInfos(_instance.FullName, searchPattern).WrapFiles();
        }

        public override IEnumerable<FileInfoBase> EnumerateFiles(string searchPattern, SearchOption searchOption)
        {
            return searchOption == SearchOption.TopDirectoryOnly
                ? Helpers.FindFilesInfos(_instance.FullName, searchPattern).WrapFiles()
                : _helpers.FindFilesInfosRecursive(_instance.FullName, searchPattern).WrapFiles();
        }

        public override IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos()
        {
            return _instance.GetFileSystemInfos().WrapFileSystemInfos();
        }

        public override IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos(string searchPattern)
        {
            return _instance.GetFileSystemInfos(searchPattern).WrapFileSystemInfos();
        }

        public override IEnumerable<FileSystemInfoBase> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
        {
            return _instance.GetFileSystemInfos(searchPattern).WrapFileSystemInfos();
        }

        public override DirectorySecurity GetAccessControl()
        {
            return _instance.GetAccessControl();
        }

        public override DirectorySecurity GetAccessControl(AccessControlSections includeSections)
        {
            return _instance.GetAccessControl();
        }

        public override DirectoryInfoBase[] GetDirectories()
        {
            return _instance.GetDirectories().WrapDirectories();
        }

        public override DirectoryInfoBase[] GetDirectories(string searchPattern)
        {
            return _instance.GetDirectories(searchPattern).WrapDirectories();
        }

        public override DirectoryInfoBase[] GetDirectories(string searchPattern, SearchOption searchOption)
        {
            return searchOption == SearchOption.TopDirectoryOnly
                ? _instance.GetDirectories(searchPattern).WrapDirectories()
                : _helpers.FindDirectoriesInfosRecursive(_instance.FullName, searchPattern).WrapDirectories();
        }

        public override FileInfoBase[] GetFiles()
        {
            return _instance.GetFiles().WrapFiles();
        }

        public override FileInfoBase[] GetFiles(string searchPattern)
        {
            return Helpers.FindFilesInfos(_instance.FullName, searchPattern).WrapFiles();
        }

        public override FileInfoBase[] GetFiles(string searchPattern, SearchOption searchOption)
        {
            return searchOption == SearchOption.TopDirectoryOnly
                ? _instance.GetFiles().WrapFiles()
                : _helpers.FindFilesInfosRecursive(_instance.FullName, searchPattern).WrapFiles();
        }

        public override FileSystemInfoBase[] GetFileSystemInfos()
        {
            return _instance.GetFileSystemInfos().WrapFileSystemInfos();
        }

        public override FileSystemInfoBase[] GetFileSystemInfos(string searchPattern)
        {
            return _instance.GetFileSystemInfos(searchPattern).WrapFileSystemInfos();
        }

        public override FileSystemInfoBase[] GetFileSystemInfos(string searchPattern, SearchOption searchOption)
        {
            return _instance.GetFileSystemInfos(searchPattern).WrapFileSystemInfos();
        }

        public override void MoveTo(string destDirName)
        {
            _instance.MoveTo(destDirName);
        }

        public override void SetAccessControl(DirectorySecurity directorySecurity)
        {
            _instance.SetAccessControl(directorySecurity);
        }

        public static implicit operator DelimonDirectoryInfoWrapper(Delimon.Win32.IO.DirectoryInfo directoryInfo)
        {
            return new DelimonDirectoryInfoWrapper(directoryInfo);
        }

    }
}