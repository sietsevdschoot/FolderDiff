using System;
using System.IO;
using System.IO.Abstractions;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    public class DelimonPathWrapper : PathBase
    {
        public override string ChangeExtension(string path, string extension)
        {
            return Delimon.Win32.IO.Path.ChangeExtension(path, extension);
        }

        public override string Combine(params string[] paths)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.DirectoryInfo");
        }

        public override string Combine(string dir, string file)
        {
            return Delimon.Win32.IO.Path.Combine(dir, file);
        }

        public override string Combine(string path1, string path2, string path3)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.DirectoryInfo");
        }

        public override string Combine(string path1, string path2, string path3, string path4)
        {
            throw new NotImplementedException("This method is not implemented by Delimon.Win32.IO.DirectoryInfo");
        }

        public override string GetDirectoryName(string path)
        {
            return Delimon.Win32.IO.Path.GetDirectoryName(path);
        }

        public override string GetExtension(string path)
        {
            return Delimon.Win32.IO.Path.GetExtension(path);
        }

        public override string GetFileName(string path)
        {
            return Delimon.Win32.IO.Path.GetFileName(path);
        }

        public override string GetFileNameWithoutExtension(string path)
        {
            return Delimon.Win32.IO.Path.GetFileNameWithoutExtension(path);
        }

        public override string GetFullPath(string path)
        {
            return Delimon.Win32.IO.Path.GetFullPath(path);
        }

        public override char[] GetInvalidFileNameChars()
        {
            return Delimon.Win32.IO.Path.GetInvalidFileNameChars();
        }

        public override char[] GetInvalidPathChars()
        {
            return Delimon.Win32.IO.Path.GetInvalidPathChars();
        }

        public override string GetPathRoot(string path)
        {
            return Delimon.Win32.IO.Path.GetPathRoot(path);
        }

        public override string GetRandomFileName()
        {
            return Delimon.Win32.IO.Path.GetRandomFileName();
        }

        public override string GetTempFileName()
        {
            return Delimon.Win32.IO.Path.GetTempFileName();
        }

        public override string GetTempPath()
        {
            return Delimon.Win32.IO.Path.GetTempPath();
        }

        public override bool HasExtension(string path)
        {
            return Delimon.Win32.IO.Path.HasExtension(path);
        }

        public override bool IsPathRooted(string path)
        {
            return Delimon.Win32.IO.Path.IsPathRooted(path);
        }

        public override char AltDirectorySeparatorChar
        {
            get { return Path.AltDirectorySeparatorChar; }
        }

        public override char DirectorySeparatorChar
        {
            get { return Path.DirectorySeparatorChar; }
        }

        public override char[] InvalidPathChars
        {
            get { return Path.GetInvalidPathChars(); }
        }

        public override char PathSeparator
        {
            get { return Path.PathSeparator; }
        }

        public override char VolumeSeparatorChar
        {
            get { return Path.VolumeSeparatorChar; }
        }
    }
}