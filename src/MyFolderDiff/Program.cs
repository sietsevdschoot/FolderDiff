using System;
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
            if (string.IsNullOrEmpty(string.Join("", args)))
            {
                Console.WriteLine(Resources.Instructions);
                Environment.Exit(0);
            }
            var arguments = ArgumentsParser.Default.ParseArguments(args);
            var factory = new FolderDiffToolFactory();
            var diffTool = factory.Create(supportLongFilenames: false);
            
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
                Console.WriteLine(Resources.Instructions);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
