using System;
using System.Diagnostics;

namespace ConfigLoaderConsole
{
    public class ArgumentWorker
    {
        public bool PathSwitchExists { get; set; }
        public bool HelpSwitchExists { get; set; }
        public bool OutputSwitchExists { get; set; }
        public bool VersionSwitchExists { get; set; }
        public string Path { get; set; }

        public ArgumentWorker(string[] args)
        {
            ParseForHelpSwitch(args);
            ParseForOutputSwitch(args);
            ParseForVersionSwitch(args);

            Debug.WriteLine("Number of arguments: " + args.Length);

            var i = 0;
            foreach (var argument in args)
            {
                if (string.Equals(argument, "--path", StringComparison.OrdinalIgnoreCase) || string.Equals(argument, "-p", StringComparison.OrdinalIgnoreCase))
                {
                    // Found the --path or -p switch, the next argument should be actual path. Save it.
                    Path = args[i + 1];
                    PathSwitchExists = true;
                }

                i++;
            }

            // If we didn't return true in the above loop
            PathSwitchExists = false;
        }

        private void ParseForHelpSwitch(string[] args)
        {
            foreach (var argument in args)
            {
                if (string.Equals(argument, "--help", StringComparison.OrdinalIgnoreCase) || string.Equals(argument, "-h", StringComparison.OrdinalIgnoreCase))
                {
                    HelpSwitchExists = true;
                }
            }
        }

        private void ParseForOutputSwitch(string[] args)
        {
            foreach (var argument in args)
            {
                if (string.Equals(argument, "--output", StringComparison.OrdinalIgnoreCase) || string.Equals(argument, "-o", StringComparison.OrdinalIgnoreCase))
                {
                    OutputSwitchExists = true;
                }
            }
        }

        private void ParseForVersionSwitch(string[] args)
        {
            foreach (var argument in args)
            {
                if (string.Equals(argument, "--version", StringComparison.OrdinalIgnoreCase) || string.Equals(argument, "-v", StringComparison.OrdinalIgnoreCase))
                {
                    VersionSwitchExists = true;
                }
            }
        }
    }
}