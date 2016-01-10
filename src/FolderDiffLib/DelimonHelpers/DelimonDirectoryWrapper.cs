using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Security.AccessControl;
using FolderDiffLib.DelimonHelpers.Util;
using SearchOption = System.IO.SearchOption;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    public class DelimonDirectoryWrapper : DirectoryBase
    {
        public override DirectoryInfoBase CreateDirectory(string path)
        {
            Delimon.Win32.IO.Directory.CreateDirectory(path);
            return new DelimonDirectoryInfoWrapper(path);
        }

        public override DirectoryInfoBase CreateDirectory(string path, DirectorySecurity directorySecurity)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override void Delete(string path)
        {
            Delimon.Win32.IO.Directory.Delete(path);
        }

        public override void Delete(string path, bool recursive)
        {
            Delimon.Win32.IO.Directory.Delete(path, recursive);
        }

        public override bool Exists(string path)
        {
            return Delimon.Win32.IO.Directory.Exists(path);
        }

        public override DirectorySecurity GetAccessControl(string path)
        {
            return Delimon.Win32.IO.Directory.GetAccessControl(path);
        }

        public override DirectorySecurity GetAccessControl(string path, AccessControlSections includeSections)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override DateTime GetCreationTime(string path)
        {
            return Delimon.Win32.IO.Directory.GetCreationTime(path);
        }

        public override DateTime GetCreationTimeUtc(string path)
        {
            return Delimon.Win32.IO.Directory.GetCreationTimeUtc(path);
        }

        public override string GetCurrentDirectory()
        {
            // Todo: Implement this correctly, supporting long filesnames
            return System.IO.Directory.GetCurrentDirectory();
        }

        public override string[] GetDirectories(string path)
        {
            return Delimon.Win32.IO.Directory.GetDirectories(path);
        }

        public override string[] GetDirectories(string path, string searchPattern)
        {
            return Delimon.Win32.IO.Directory.GetDirectories(path, searchPattern);
        }

        public override string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            var options = MyConverters.ConvertEnum<SearchOption, Delimon.Win32.IO.SearchOption>(searchOption);

            return Delimon.Win32.IO.Directory.GetDirectories(path, searchPattern, options);
        }

        public override string GetDirectoryRoot(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override string[] GetFiles(string path)
        {
            return Delimon.Win32.IO.Directory.GetFiles(path);
        }

        public override string[] GetFiles(string path, string searchPattern)
        {
            return Delimon.Win32.IO.Directory.GetFiles(path, searchPattern);
        }

        public override string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            var options = MyConverters.ConvertEnum<SearchOption, Delimon.Win32.IO.SearchOption>(searchOption);

            return Delimon.Win32.IO.Directory.GetFiles(path, searchPattern, options);
        }

        public override string[] GetFileSystemEntries(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override string[] GetFileSystemEntries(string path, string searchPattern)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override DateTime GetLastAccessTime(string path)
        {
            return Delimon.Win32.IO.Directory.GetLastAccessTime(path);
        }

        public override DateTime GetLastAccessTimeUtc(string path)
        {
            return Delimon.Win32.IO.Directory.GetLastAccessTimeUtc(path);
        }

        public override DateTime GetLastWriteTime(string path)
        {
            return Delimon.Win32.IO.Directory.GetLastWriteTime(path);
        }

        public override DateTime GetLastWriteTimeUtc(string path)
        {
            return Delimon.Win32.IO.Directory.GetLastWriteTimeUtc(path);
        }

        public override string[] GetLogicalDrives()
        {
            return Delimon.Win32.IO.Directory.GetLogicalDrives();
        }

        public override DirectoryInfoBase GetParent(string path)
        {
            var directoryInfo = Delimon.Win32.IO.Directory.GetParent(path);
            return new DelimonDirectoryInfoWrapper(directoryInfo);
        }

        public override void Move(string sourceDirName, string destDirName)
        {
            Delimon.Win32.IO.Directory.Move(sourceDirName, destDirName);
        }

        public override void SetAccessControl(string path, DirectorySecurity directorySecurity)
        {
            Delimon.Win32.IO.Directory.SetAccessControl(path, directorySecurity);
        }

        public override void SetCreationTime(string path, DateTime creationTime)
        {
            Delimon.Win32.IO.Directory.SetCreationTime(path, creationTime);
        }

        public override void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        {
            Delimon.Win32.IO.Directory.SetCreationTimeUtc(path, creationTimeUtc);
        }

        public override void SetCurrentDirectory(string path)
        {
            // Todo: Implement this correctly, supporting long filesnames
            System.IO.Directory.SetCurrentDirectory(path);
        }

        public override void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            Delimon.Win32.IO.Directory.SetLastAccessTime(path, lastAccessTime);
        }

        public override void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            Delimon.Win32.IO.Directory.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
        }

        public override void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            Delimon.Win32.IO.Directory.SetLastWriteTime(path, lastWriteTime);
        }

        public override void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            Delimon.Win32.IO.Directory.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
        }

        public override IEnumerable<string> EnumerateDirectories(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override IEnumerable<string> EnumerateFiles(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override IEnumerable<string> EnumerateFileSystemEntries(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }

        public override IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.Directory");
        }
    }
}