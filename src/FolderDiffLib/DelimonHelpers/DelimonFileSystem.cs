using System;
using System.IO.Abstractions;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    public class DelimonFileSystem : IFileSystem
    {
        private DelimonDirectoryWrapper _directory;
        private DelimonFileWrapper _file;
        private DelimonFileInfoFactory _fileInfoFactory;
        private DelimonPathWrapper _path;
        private DelimonDirectoryInfoFactory _directoryInfoFactory;

        public DirectoryBase Directory
        {
            get { return _directory ?? (_directory = new DelimonDirectoryWrapper()); }
        }

        public FileBase File
        {
            get { return _file ?? (_file = new DelimonFileWrapper()); }
        }

        public IFileInfoFactory FileInfo
        {
            get { return _fileInfoFactory ?? (_fileInfoFactory = new DelimonFileInfoFactory()); }
        }
        
        public PathBase Path
        {
            get { return _path ?? (_path = new DelimonPathWrapper()); }
        }
        
        public IDirectoryInfoFactory DirectoryInfo
        {
            get { return _directoryInfoFactory ?? (_directoryInfoFactory = new DelimonDirectoryInfoFactory()); }
        }
    }
}