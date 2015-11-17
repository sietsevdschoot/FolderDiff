using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Linq;
using Delimon.Win32.IO;
using FolderDiffLib.Common;

namespace FolderDiffLib.DelimonHelpers.Util
{
    public static class MyConverters
    {
        public static TDestEnum ConvertEnum<TSrcEnum, TDestEnum>(TSrcEnum value)
            where TSrcEnum : struct
            where TDestEnum : struct
        {
            var availableValues = Enum.GetNames(typeof (TSrcEnum));

            if (!availableValues.Contains(value.ToString()))
                throw new NotSupportedException(string.Format("Can't cast '{0}' to {1}", value, typeof(TDestEnum).FullName));

            return (TDestEnum) Enum.Parse(typeof (TDestEnum), value.ToString());
        }


        internal static FileSystemInfoBase[] WrapFileSystemInfos(this IEnumerable<FileSystemInfo> input)
        {
            return input
                .Select<FileSystemInfo, FileSystemInfoBase>(item =>
                {
                    if (item is FileInfo)
                        return (FileInfoBase)new DelimonFileInfoWrapper((FileInfo)item);

                    if (item is DirectoryInfo)
                        return (DirectoryInfoBase)new DelimonDirectoryInfoWrapper((DirectoryInfo)item);

                    throw new NotImplementedException(string.Format(
                        CultureInfo.InvariantCulture,
                        "The type {0} is not recognized by the System.IO.Abstractions library.",
                        item.GetType().AssemblyQualifiedName
                    ));
                })
                .ToArray();
        }

        internal static DirectoryInfoBase[] WrapDirectories(this IEnumerable<Delimon.Win32.IO.DirectoryInfo> input)
        {
            return input.Select(f => (DirectoryInfoBase)new DelimonDirectoryInfoWrapper(f)).ToArray();
        }

        internal static FileInfoBase[] WrapFiles(this IEnumerable<Delimon.Win32.IO.FileInfo> input)
        {
            return input.Select(f => (FileInfoBase)new DelimonFileInfoWrapper(f)).ToArray();
        }
    }
}