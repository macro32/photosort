using CommandLine;
using CommandLine.Text;

namespace photosort
{
    public class Options
    {
        [Option('r', "read", Required = true,
            HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('v', "verbose", DefaultValue = true,
            HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('s', "source", HelpText="Root of input tree containing files.")]
        public string Source { get; set; }

        [Option('d', "destination", HelpText = "Root of destination tree.")]
        public string Destination { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

    }
}
