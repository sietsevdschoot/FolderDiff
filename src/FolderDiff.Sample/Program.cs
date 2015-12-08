using System;
using System.Linq;
using FolderDiff.Sample.Properties;
using FolderDiffLib;

namespace FolderDiff.Sample
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
            var factory = new FolderDiffToolFactory();
            var diffTool = factory.Create(supportLongFilenames: true);
            
            var runner = new Runner(diffTool);

            runner.GetNewerAndUniqueDiffFiles(arguments.ReferenceFolder, arguments.DiffFolder);
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
