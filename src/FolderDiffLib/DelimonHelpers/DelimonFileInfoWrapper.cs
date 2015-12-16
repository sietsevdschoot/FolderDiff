using System;
using System.IO;
using System.IO.Abstractions;
using System.Security.AccessControl;
using FolderDiffLib.DelimonHelpers.Util;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    public class DelimonFileInfoWrapper : FileInfoBase
    {
        private readonly Delimon.Win32.IO.FileInfo _instance;

        public DelimonFileInfoWrapper(Delimon.Win32.IO.FileInfo instance)
        {
            _instance = instance;
        }

        public DelimonFileInfoWrapper(string fullName)
        {
            _instance = new Delimon.Win32.IO.FileInfo(fullName);
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
            get { return _instance.Extension; }
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

        public override DirectoryInfoBase Directory
        {
            get { return new DelimonDirectoryInfoWrapper(_instance.Directory); }
        }

        public override string DirectoryName
        {
            get { return _instance.DirectoryName; }
        }

        public override bool IsReadOnly
        {
            get { return _instance.IsReadOnly; }
            set { _instance.IsReadOnly = value; }
        }

        public override long Length
        {
            get { return _instance.Length; }
        }

        public override void Delete()
        {
            _instance.Delete();
        }

        public override void Refresh()
        {
            _instance.Refresh();
        }

        public override StreamWriter AppendText()
        {
            return _instance.AppendText();
        }

        public override FileInfoBase CopyTo(string destFileName)
        {
            _instance.CopyTo(destFileName);
            return new DelimonFileInfoWrapper(destFileName);
        }

        public override FileInfoBase CopyTo(string destFileName, bool overwrite)
        {
            _instance.CopyTo(destFileName, overwrite);
            return new DelimonFileInfoWrapper(destFileName);
        }

        public override Stream Create()
        {
            return _instance.Create();
        }

        public override StreamWriter CreateText()
        {
            return _instance.CreateText();
        }

        public override void Decrypt()
        {
            _instance.Decrypt();
        }

        public override void Encrypt()
        {
            _instance.Encrypt();
        }

        public override FileSecurity GetAccessControl()
        {
            return _instance.GetAccessControl();
        }

        public override FileSecurity GetAccessControl(AccessControlSections includeSections)
        {
            return _instance.GetAccessControl(includeSections);
        }

        public override void MoveTo(string destFileName)
        {
            _instance.MoveTo(destFileName);
        }

        public override Stream Open(FileMode mode)
        {
            var fileMode = MyConverters.ConvertEnum<System.IO.FileMode, Delimon.Win32.IO.FileMode>(mode);
            return _instance.Open(fileMode);
        }

        public override Stream Open(FileMode mode, FileAccess access)
        {
            var fileMode = MyConverters.ConvertEnum<System.IO.FileMode, Delimon.Win32.IO.FileMode>(mode);
            var fileAccess = MyConverters.ConvertEnum<System.IO.FileAccess, Delimon.Win32.IO.FileAccess>(access);

            return _instance.Open(fileMode, fileAccess);
        }

        public override Stream Open(FileMode mode, FileAccess access, FileShare share)
        {
            var fileMode = MyConverters.ConvertEnum<System.IO.FileMode, Delimon.Win32.IO.FileMode>(mode);
            var fileAccess = MyConverters.ConvertEnum<System.IO.FileAccess, Delimon.Win32.IO.FileAccess>(access);
            var fileShare = MyConverters.ConvertEnum<System.IO.FileShare, Delimon.Win32.IO.FileShare>(share);

            return _instance.Open(fileMode, fileAccess, fileShare);
        }

        public override Stream OpenRead()
        {
            return _instance.OpenRead();
        }

        public override StreamReader OpenText()
        {
            return _instance.OpenText();
        }

        public override Stream OpenWrite()
        {
            return _instance.OpenWrite();
        }

        public override FileInfoBase Replace(string destinationFileName, string destinationBackupFileName)
        {
            var instance = _instance.Replace(destinationFileName, destinationBackupFileName);
            return new DelimonFileInfoWrapper(instance);
        }

        public override FileInfoBase Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            var instance = _instance.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
            return new DelimonFileInfoWrapper(instance);
        }

        public override void SetAccessControl(FileSecurity fileSecurity)
        {
            _instance.SetAccessControl(fileSecurity);
        }

        public static implicit operator DelimonFileInfoWrapper(Delimon.Win32.IO.FileInfo fileInfo)
        {
            return new DelimonFileInfoWrapper(fileInfo);
        }
    }
}