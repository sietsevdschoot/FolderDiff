using FluentAssertions;
using FolderDiffLib.Sample;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FolderDiffLibTests
{
    [TestClass]
    public class ArgumentsParserTests
    {
        private const string Folder1 = @"c:\Temp\Folder1\";
        private const string Folder1WithSpace = @"c:\Temp\Folder1\Sub Folder1\";
        
        private const string Folder2 = @"c:\Temp\Folder2\";
        private const string Folder2WithSpace = @"c:\Temp\Folder1\Sub Folder2\";
        
        [TestMethod]
        public void ParseArguments_ParsesPathWithoutSpaces()
        {
            var args = GetParsedArgument(string.Format("{0} {1}", Folder1, Folder2));

            args.ReferenceFolder.Should().Be(Folder1);
            args.DiffFolder.Should().Be(Folder2);
        }

        [TestMethod]
        public void ParseArguments_ParsesPathWithSpaces()
        {
            var args = GetParsedArgument(string.Format("\"{0}\" \"{1}\"", Folder1WithSpace, Folder2WithSpace));

            args.ReferenceFolder.Should().Be(Folder1WithSpace);
            args.DiffFolder.Should().Be(Folder2WithSpace);
        }

        [TestMethod]
        public void ParseArguments_ParsesPathWithAndWIthoutSpaces()
        {
            var args = GetParsedArgument(string.Format("\"{0}\" {1}", Folder1WithSpace, Folder2));

            args.ReferenceFolder.Should().Be(Folder1WithSpace);
            args.DiffFolder.Should().Be(Folder2);
        }

        [TestMethod]
        public void ParseArguments_ParsesPathWithAndWIthoutSpaces_OtherVariant()
        {
            var args = GetParsedArgument(string.Format("{0} \"{1}\"", Folder1, Folder2WithSpace));

            args.ReferenceFolder.Should().Be(Folder1);
            args.DiffFolder.Should().Be(Folder2WithSpace);
        }
        
        private Arguments GetParsedArgument(string arg)
        {
            var parser = new ArgumentsParser();
            var args = arg.Split(' ');

            return parser.ParseArguments(args);
        }
    }
}
