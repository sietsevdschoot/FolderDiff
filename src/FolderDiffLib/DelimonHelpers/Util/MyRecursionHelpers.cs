using System.Collections.Generic;
using System.Linq;
using Delimon.Win32.IO;

namespace FolderDiffLib.DelimonHelpers.Util
{
    public class MyRecursionHelpers
    {
        private readonly DelimonHelpersBase _helpers;

        public MyRecursionHelpers(DelimonHelpersBase helpers)
        {
            _helpers = helpers;
        }

        public IEnumerable<Delimon.Win32.IO.DirectoryInfo> FindDirectoriesInfosRecursive(string source, string searchPattern)
        {
            var directories = _helpers.FindDirectoriesInfos(source, searchPattern);

            foreach (var directoryInfo in directories)
            {
                yield return directoryInfo;

                foreach (var subFolder in FindDirectoriesInfosRecursive(directoryInfo.FullName, searchPattern))
                {
                    yield return subFolder;
                }
            }
        }

        public IEnumerable<Delimon.Win32.IO.FileInfo> FindFilesInfosRecursive(string source, string searchPattern)
        {
            var files = _helpers.FindFilesInfos(source, searchPattern);

            foreach (var file in files)
            {
                yield return file;
            }

            foreach (var subFolder in FindDirectoriesInfosRecursive(source, "*"))
            {
                var filesInSubFolder = _helpers.FindFilesInfos(subFolder.FullName, searchPattern);

                foreach (var fileInSubfolder in filesInSubFolder)  
                {
                    yield return fileInSubfolder;
                }
            }
        }
    }
}