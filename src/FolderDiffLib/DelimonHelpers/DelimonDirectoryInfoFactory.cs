using System;
using System.IO.Abstractions;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    internal class DelimonDirectoryInfoFactory : IDirectoryInfoFactory
    {
        public DirectoryInfoBase FromDirectoryName(string directoryName)
        {
            var realDirectoryInfo = new Delimon.Win32.IO.DirectoryInfo(directoryName);
            return new DelimonDirectoryInfoWrapper(realDirectoryInfo);
        }
    }
}