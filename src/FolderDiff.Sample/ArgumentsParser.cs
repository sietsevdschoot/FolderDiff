using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderDiff.Sample
{
    public class ArgumentsParser
    {
        public Arguments ParseArguments(string[] args)
        {
            var arg = string.Join(" ", args);

            var myArgs = Split(arg, '"').Count == 1
                ? Split(arg, ' ')
                : Split(arg, '"');

            if (myArgs.Count != 2)
                throw new ArgumentException("Invalid input");
            
            return new Arguments
            {
                ReferenceFolder = myArgs[0],
                DiffFolder = myArgs[1]
            };
        }

        private List<string> Split(string str, char separator)
        {
            return str.Split(separator).Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public static ArgumentsParser Default
        {
            get { return new ArgumentsParser() ;}
        }
    }

    public class Arguments
    {
        public string ReferenceFolder { get; set; }
        public string DiffFolder { get; set; }
    }

}