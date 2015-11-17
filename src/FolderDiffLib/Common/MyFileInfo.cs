using System.Diagnostics;
using System.IO.Abstractions;

namespace FolderDiffLib.Common
{
    [DebuggerDisplay("{RelativePath} - {File.FullName}")]
    public class MyFileInfo
    {
        public MyFileInfo(FileInfoBase fileInfo, string path)
        {
            RelativePath = fileInfo.FullName.Replace(path, string.Empty);
            File = fileInfo;
        }

        public string RelativePath { get; set; }
        public FileInfoBase File { get; set; }
    }
}