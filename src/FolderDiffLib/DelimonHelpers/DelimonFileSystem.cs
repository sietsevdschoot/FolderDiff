using System;
using System.IO.Abstractions;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    public class DelimonFileSystem : IFileSystem
    {
        private DirectoryBase _directory;
        private FileBase _file;
        private DelimonFileInfoFactory _fileInfoFactory;
        private PathBase _path;
        private DelimonDirectoryInfoFactory _directoryInfoFactory;

        public DirectoryBase Directory
        {
            get { return _directory ?? (_directory = new DirectoryWrapper()); }
        }

        public FileBase File
        {
            get { return _file ?? (_file = new FileWrapper()); }
        }

        public IFileInfoFactory FileInfo
        {
            get { return _fileInfoFactory ?? (_fileInfoFactory = new DelimonFileInfoFactory()); }
        }
        
        public PathBase Path
        {
            get { return _path ?? (_path = new PathWrapper()); }
        }
        
        public IDirectoryInfoFactory DirectoryInfo
        {
            get { return _directoryInfoFactory ?? (_directoryInfoFactory = new DelimonDirectoryInfoFactory()); }
        }
    }
}