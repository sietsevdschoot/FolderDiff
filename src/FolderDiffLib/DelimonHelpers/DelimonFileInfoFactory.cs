using System;
using System.IO.Abstractions;

namespace FolderDiffLib.DelimonHelpers
{
    [Serializable]
    internal class DelimonFileInfoFactory : IFileInfoFactory
    {
        public FileInfoBase FromFileName(string fileName)
        {
            var realFileInfo = new Delimon.Win32.IO.FileInfo(fileName);
            return new DelimonFileInfoWrapper(realFileInfo);
        }
    }
}