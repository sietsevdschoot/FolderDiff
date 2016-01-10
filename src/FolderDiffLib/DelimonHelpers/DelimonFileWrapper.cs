using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using FolderDiffLib.DelimonHelpers.Util;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    public class DelimonFileWrapper : FileBase
    {
        public override void AppendAllLines(string path, IEnumerable<string> contents)
        {
            Delimon.Win32.IO.File.AppendAllText(path, string.Join(Environment.NewLine, contents));
        }

        public override void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            Delimon.Win32.IO.File.AppendAllText(path, string.Join(Environment.NewLine, contents), encoding);
        }

        public override void AppendAllText(string path, string contents)
        {
            Delimon.Win32.IO.File.AppendAllText(path, contents);
        }

        public override void AppendAllText(string path, string contents, Encoding encoding)
        {
            Delimon.Win32.IO.File.AppendAllText(path, contents, encoding);
        }

        public override StreamWriter AppendText(string path)
        {
            return Delimon.Win32.IO.File.AppendText(path);
        }

        public override void Copy(string sourceFileName, string destFileName)
        {
            Delimon.Win32.IO.File.Copy(sourceFileName, destFileName, overwrite: false);
        }

        public override void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            Delimon.Win32.IO.File.Copy(sourceFileName, destFileName, overwrite);
        }

        public override Stream Create(string path)
        {
            return Delimon.Win32.IO.File.Create(path);
        }

        public override Stream Create(string path, int bufferSize)
        {
            return Delimon.Win32.IO.File.Create(path, bufferSize);
        }

        public override Stream Create(string path, int bufferSize, FileOptions options)
        {
            var fileOptions = MyConverters.ConvertEnum<FileOptions, Delimon.Win32.IO.FileOptions>(options);

            return Delimon.Win32.IO.File.Create(path, bufferSize, fileOptions);
        }

        public override Stream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }

        public override StreamWriter CreateText(string path)
        {
            return Delimon.Win32.IO.File.CreateText(path);
        }

        public override void Decrypt(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }

        public override void Delete(string path)
        {
            Delimon.Win32.IO.File.Delete(path);
        }

        public override void Encrypt(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }

        public override bool Exists(string path)
        {
            return Delimon.Win32.IO.File.Exists(path);
        }

        public override FileSecurity GetAccessControl(string path)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }

        public override FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }

        public override FileAttributes GetAttributes(string path)
        {
            var attributes = Delimon.Win32.IO.File.GetAttributes(path);

            return MyConverters.ConvertEnum<Delimon.Win32.IO.FileAttributes, FileAttributes>(attributes);
        }

        public override DateTime GetCreationTime(string path)
        {
            return Delimon.Win32.IO.File.GetCreationTime(path);
        }

        public override DateTime GetCreationTimeUtc(string path)
        {
            return Delimon.Win32.IO.File.GetCreationTimeUtc(path);
        }

        public override DateTime GetLastAccessTime(string path)
        {
            return Delimon.Win32.IO.File.GetLastAccessTime(path);
        }

        public override DateTime GetLastAccessTimeUtc(string path)
        {
            return Delimon.Win32.IO.File.GetLastAccessTimeUtc(path);
        }

        public override DateTime GetLastWriteTime(string path)
        {
            return Delimon.Win32.IO.File.GetLastWriteTime(path);
        }

        public override DateTime GetLastWriteTimeUtc(string path)
        {
            return Delimon.Win32.IO.File.GetLastWriteTimeUtc(path);
        }

        public override void Move(string sourceFileName, string destFileName)
        {
            Delimon.Win32.IO.File.Move(sourceFileName, destFileName);
        }

        public override Stream Open(string path, FileMode mode)
        {
            var fileMode = MyConverters.ConvertEnum<FileMode, Delimon.Win32.IO.FileMode>(mode);

            return Delimon.Win32.IO.File.Open(path, fileMode);
        }

        public override Stream Open(string path, FileMode mode, FileAccess access)
        {
            var fileMode = MyConverters.ConvertEnum<FileMode, Delimon.Win32.IO.FileMode>(mode);
            var fileAccess = MyConverters.ConvertEnum<FileAccess, Delimon.Win32.IO.FileAccess>(access);

            return Delimon.Win32.IO.File.Open(path, fileMode, fileAccess);
        }

        public override Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            var fileMode = MyConverters.ConvertEnum<FileMode, Delimon.Win32.IO.FileMode>(mode);
            var fileAccess = MyConverters.ConvertEnum<FileAccess, Delimon.Win32.IO.FileAccess>(access);
            var fileShare = MyConverters.ConvertEnum<FileShare, Delimon.Win32.IO.FileShare>(share);

            return Delimon.Win32.IO.File.Open(path, fileMode, fileAccess, fileShare);
        }

        public override Stream OpenRead(string path)
        {
            return Delimon.Win32.IO.File.OpenRead(path);
        }

        public override StreamReader OpenText(string path)
        {
            return Delimon.Win32.IO.File.OpenText(path);
        }

        public override Stream OpenWrite(string path)
        {
            return Delimon.Win32.IO.File.OpenWrite(path);
        }

        public override byte[] ReadAllBytes(string path)
        {
            return Delimon.Win32.IO.File.ReadAllBytes(path);
        }

        public override string[] ReadAllLines(string path)
        {
            return Delimon.Win32.IO.File.ReadAllLines(path);
        }

        public override string[] ReadAllLines(string path, Encoding encoding)
        {
            return Delimon.Win32.IO.File.ReadAllLines(path, encoding);
        }

        public override string ReadAllText(string path)
        {
            return Delimon.Win32.IO.File.ReadAllText(path);
        }

        public override string ReadAllText(string path, Encoding encoding)
        {
            return Delimon.Win32.IO.File.ReadAllText(path, encoding);
        }

        public override IEnumerable<string> ReadLines(string path)
        {
            return Delimon.Win32.IO.File.ReadAllLines(path);
        }

        public override IEnumerable<string> ReadLines(string path, Encoding encoding)
        {
            return Delimon.Win32.IO.File.ReadAllLines(path, encoding);
        }

        public override void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }

        public override void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName,
            bool ignoreMetadataErrors)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }

        public override void SetAccessControl(string path, FileSecurity fileSecurity)
        {
            var file = new Delimon.Win32.IO.File();
            file.SetAccessControl(path, fileSecurity);
        }

        public override void SetAttributes(string path, FileAttributes fileAttributes)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }
        
        public override void SetCreationTime(string path, DateTime creationTime)
        {
            Delimon.Win32.IO.File.SetCreationTime(path, creationTime);
        }

        public override void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        {
            Delimon.Win32.IO.File.SetCreationTimeUtc(path, creationTimeUtc);
        }

        public override void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            Delimon.Win32.IO.File.SetLastAccessTime(path, lastAccessTime);
        }

        public override void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        {
            Delimon.Win32.IO.File.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
        }

        public override void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            Delimon.Win32.IO.File.SetLastWriteTime(path, lastWriteTime);
        }

        public override void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            Delimon.Win32.IO.File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
        }

        public override void WriteAllBytes(string path, byte[] bytes)
        {
            Delimon.Win32.IO.File.WriteAllBytes(path, bytes);
        }

        public override void WriteAllLines(string path, IEnumerable<string> contents)
        {
            Delimon.Win32.IO.File.WriteAllLines(path, contents.ToArray());
        }

        public override void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            Delimon.Win32.IO.File.WriteAllLines(path, contents.ToArray(), encoding);
        }

        public override void WriteAllLines(string path, string[] contents)
        {
            Delimon.Win32.IO.File.WriteAllLines(path, contents);
        }

        public override void WriteAllLines(string path, string[] contents, Encoding encoding)
        {
            Delimon.Win32.IO.File.WriteAllLines(path, contents, encoding);
        }

        public override void WriteAllText(string path, string contents)
        {
            WriteAllLines(path, contents.Split(new [] {Environment.NewLine}, StringSplitOptions.None));
        }

        public override void WriteAllText(string path, string contents, Encoding encoding)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.File");
        }
    }
}