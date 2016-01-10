using System;
using System.Linq;
using FolderDiffLib.DiffTools;
using FolderDiffLib.Sample.Properties;

namespace FolderDiffLib.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Try(() => RunApp(args));
        }

        private static void RunApp(string[] args)
        {
            if (!args.Any() || args[0] == "/?")
            {
                Console.WriteLine(Resources.Instructions);
                Environment.Exit(0);
            }
            var arguments = ArgumentsParser.Default.ParseArguments(args);
            
            var fileSystem = FileSystemFactory.Create(supportLongFilenames: false);
            var diffTool = FolderDiffFactory.Create(fileSystem);
            var fileSystemHelper = new FileSystemHelper(fileSystem);
            
            var runner = new Runner(diffTool, fileSystemHelper);

            Console.WriteLine(runner.GetNewerAndUniqueDiffFiles(arguments.ReferenceFolder, arguments.DiffFolder));
        }

        private static void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
