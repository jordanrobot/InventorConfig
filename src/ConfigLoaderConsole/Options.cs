using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace ConfigLoaderConsole
{
    class Options
    {
        [Option("path", Required = false, HelpText = "Specify a filename of a configuration json file to load into Inventor.  May be a relative or absolute path.")]
        public string path { get; set; }

        [Option("test", Required = false, HelpText = "Test json configuration file for valid format - Inventor settings will not be modified.")]
        public bool test { get; set; }

        [Option("output", Required = false, HelpText = "FUTURE FEATURE: Specify a destination filename; writes a json file of the existing Inventor application configuration.  May be a relative or absolute path.")]
        public string output { get; set; }

        [Usage(ApplicationAlias = "config-loader")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>()
                {
                new Example("Load a json configuration file into Inventor", new Options { path = "config-loader.json" }),
                new Example("Write a json configuration file from Inventor", new Options { output = "config-loader.json" }),
                new Example("Verify a json configuration file has no errors", new Options { path = "config-loader.json", test = true })
                };
            }
        }
    }
}
