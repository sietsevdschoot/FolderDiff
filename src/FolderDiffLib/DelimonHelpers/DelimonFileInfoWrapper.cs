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

        public override FileAttributes Attributes
        {
            get
            {
                return MyConverters.ConvertEnum<Delimon.Win32.IO.FileAttributes, FileAttributes>(this._instance.Attributes);
            }
            set
            {
                this._instance.Attributes = MyConverters.ConvertEnum<FileAttributes, Delimon.Win32.IO.FileAttributes>(value);
            }
        }

        public override DateTime CreationTime
        {
            get
            {
                return this._instance.CreationTime;
            }
            set
            {
                this._instance.CreationTime = value;
            }
        }

        public override DateTime CreationTimeUtc
        {
            get
            {
                return this._instance.CreationTimeUtc;
            }
            set
            {
                this._instance.CreationTimeUtc = value;
            }
        }

        public override bool Exists
        {
            get
            {
                return this._instance.Exists;
            }
        }

        public override string Extension
        {
            get
            {
                return this._instance.Extension;
            }
        }

        public override string FullName
        {
            get
            {
                return this._instance.FullName;
            }
        }

        public override DateTime LastAccessTime
        {
            get
            {
                return this._instance.LastAccessTime;
            }
            set
            {
                this._instance.LastAccessTime = value;
            }
        }

        public override DateTime LastAccessTimeUtc
        {
            get
            {
                return this._instance.LastAccessTimeUtc;
            }
            set
            {
                this._instance.LastAccessTimeUtc = value;
            }
        }

        public override DateTime LastWriteTime
        {
            get
            {
                return this._instance.LastWriteTime;
            }
            set
            {
                this._instance.LastWriteTime = value;
            }
        }

        public override DateTime LastWriteTimeUtc
        {
            get
            {
                return this._instance.LastWriteTimeUtc;
            }
            set
            {
                this._instance.LastWriteTimeUtc = value;
            }
        }

        public override string Name
        {
            get
            {
                return this._instance.Name;
            }
        }

        public override DirectoryInfoBase Directory
        {
            get
            {
                return new DelimonDirectoryInfoWrapper(this._instance.Directory);
            }
        }

        public override string DirectoryName
        {
            get
            {
                return this._instance.DirectoryName;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this._instance.IsReadOnly;
            }
            set
            {
                this._instance.IsReadOnly = value;
            }
        }

        public override long Length
        {
            get
            {
                return this._instance.Length;
            }
        }

        public override void Delete()
        {
            this._instance.Delete();
        }

        public override void Refresh()
        {
            this._instance.Refresh();
        }

        public override StreamWriter AppendText()
        {
            return this._instance.AppendText();
        }

        public override FileInfoBase CopyTo(string destFileName)
        {
            this._instance.CopyTo(destFileName);
            return new DelimonFileInfoWrapper(destFileName);
        }

        public override FileInfoBase CopyTo(string destFileName, bool overwrite)
        {
            this._instance.CopyTo(destFileName, overwrite);
            return new DelimonFileInfoWrapper(destFileName);
        }

        public override Stream Create()
        {
            return (Stream)this._instance.Create();
        }

        public override StreamWriter CreateText()
        {
            return this._instance.CreateText();
        }

        public override void Decrypt()
        {
            this._instance.Decrypt();
        }

        public override void Encrypt()
        {
            this._instance.Encrypt();
        }

        public override FileSecurity GetAccessControl()
        {
            return this._instance.GetAccessControl();
        }

        public override FileSecurity GetAccessControl(AccessControlSections includeSections)
        {
            return this._instance.GetAccessControl(includeSections);
        }

        public override void MoveTo(string destFileName)
        {
            this._instance.MoveTo(destFileName);
        }

        public override Stream Open(FileMode mode)
        {
            var fileMode = MyConverters.ConvertEnum<FileMode, Delimon.Win32.IO.FileMode>(mode);
            return (Stream)this._instance.Open(fileMode);
        }

        public override Stream Open(FileMode mode, FileAccess access)
        {
            var fileMode = MyConverters.ConvertEnum<FileMode, Delimon.Win32.IO.FileMode>(mode);
            var fileAccess = MyConverters.ConvertEnum<FileAccess, Delimon.Win32.IO.FileAccess>(access);

            return (Stream)this._instance.Open(fileMode, fileAccess);
        }

        public override Stream Open(FileMode mode, FileAccess access, FileShare share)
        {
            var fileMode = MyConverters.ConvertEnum<FileMode, Delimon.Win32.IO.FileMode>(mode);
            var fileAccess = MyConverters.ConvertEnum<FileAccess, Delimon.Win32.IO.FileAccess>(access);
            var fileShare = MyConverters.ConvertEnum<FileShare, Delimon.Win32.IO.FileShare>(share);

            return (Stream)this._instance.Open(fileMode, fileAccess, fileShare);
        }

        public override Stream OpenRead()
        {
            return (Stream)this._instance.OpenRead();
        }

        public override StreamReader OpenText()
        {
            return this._instance.OpenText();
        }

        public override Stream OpenWrite()
        {
            return (Stream)this._instance.OpenWrite();
        }

        public override FileInfoBase Replace(string destinationFileName, string destinationBackupFileName)
        {
            var instance = this._instance.Replace(destinationFileName, destinationBackupFileName);

            return new DelimonFileInfoWrapper(instance);
        }

        public override FileInfoBase Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            var instance = this._instance.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
            return new DelimonFileInfoWrapper(instance);
        }

        public override void SetAccessControl(FileSecurity fileSecurity)
        {
            this._instance.SetAccessControl(fileSecurity);
        }

        public static implicit operator DelimonFileInfoWrapper(Delimon.Win32.IO.FileInfo fileInfo)
        {
            return new DelimonFileInfoWrapper(fileInfo);
        }
    }
}