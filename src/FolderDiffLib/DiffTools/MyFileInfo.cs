using System.Diagnostics;
using System.IO.Abstractions;

namespace FolderDiffLib.DiffTools
{
    [DebuggerDisplay("{RelativePath} - {File.FullName}")]
    public class MyFileInfo
    {
        public MyFileInfo(FileInfoBase fileInfo, string path)
        {
            RelativePath = fileInfo.FullName.ToLowerInvariant().Replace(path.ToLowerInvariant(), string.Empty).TrimStart('\\');
            File = fileInfo;
        }

        public string RelativePath { get; set; }
        public FileInfoBase File { get; set; }

        public static implicit operator FileInfoBase(MyFileInfo instance)
        {
            return instance.File;
        }
    }
}