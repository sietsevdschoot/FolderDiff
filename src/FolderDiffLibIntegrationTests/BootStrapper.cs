using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FolderDiffLibIntegrationTests
{
    [TestClass]
    public class BootStrapper
    {
        [AssemblyInitialize]
        public static void AsssemblyInitialize(TestContext context)
        {
            var testTempFolder = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "FolderDiff"));

            if (testTempFolder.Exists)
            {
                var oldDirectories = testTempFolder.GetDirectories("*", SearchOption.TopDirectoryOnly)
                                                   .Where(x => DateTime.UtcNow - x.CreationTime > TimeSpan.FromMinutes(3))
                                                   .ToList();

                oldDirectories.ForEach(x => x.Delete(recursive: true));
            
            }

        }
    }
}