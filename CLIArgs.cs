using CommandLine;
using System.IO;

namespace CTRInfo
{
    public class CLIArgs
    {
        [Verb("cia", HelpText = "Read a CIA from a given path.")]
        public class SingleCiaReadOptions
        {
            [Option('p', "path", Required = true, HelpText = "The path to the CIA to be analyzed.")]
            public string CiaPath { get; set; }

            [Option('g', "gm9-name-format", Required = false, HelpText = "(Optional) Change the file name of the given CIA to the GodMode9-style naming scheme.")]
            public bool ChangeToGm9Format { get; set; } = false;

            [Option('n', "use-ninfs", Required = false, HelpText = "(Optional) Use ninfs to also read the title names of the CIA. (Can only be used if -n (--ninfs) is specified.)")]
            public bool UseNinfs { get; set; } = false;

            [Option('v', "verbose", Required = false, HelpText = "Show very detailed information about every CIA. (this will also write the output to a file, with it's name being the name of the CIA)")]
            public bool Verbose { get; set; } = false;
        }

        [Verb("dir", HelpText = "Read all CIAs in a given directory.")]
        public class DirectoryReadOptions
        {
            [Option('d', "dir", Required = true, HelpText = "The directory containing the CIAs to be analyzed.")]
            public string CiaDirPath { get; set; }

            [Option('g', "gm9-name-format", Required = false, HelpText = "(Optional) Change the file name of the given CIA to the GodMode9-style naming scheme.")]
            public bool ChangeToGm9Format { get; set; } = false;

            [Option('n', "use-ninfs", Required = false, HelpText = "(Optional) Use ninfs to also read the title names of the CIA. (Can only be used if -n (--ninfs) is specified.)")]
            public bool UseNinfs { get; set; } = false;

            [Option('v', "verbose", Required = false, HelpText = "Show very detailed information about every CIA. (this will also write the output to a file, with it's name being the name of the CIA)")]
            public bool Verbose { get; set; } = false;
        }
    }
}