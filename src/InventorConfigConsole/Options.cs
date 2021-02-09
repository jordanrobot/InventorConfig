using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace InventorConfigConsole
{
    class Options
    {
        [Option("path", Required = false, HelpText = "Specify a filename of a configuration JSON file to load into Inventor.  May be a relative or absolute path.")]
        public string path { get; set; }

        [Option("test", Required = false, HelpText = "Test JSON configuration file for valid format - Inventor settings will not be modified.")]
        public bool test { get; set; }

        [Option("output", Required = false, HelpText = "Specify a destination filename; writes a JSON file of the existing Inventor application configuration.  May be a relative or absolute path.")]
        public string output { get; set; }

        [Usage(ApplicationAlias = "inventor-config")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>()
                {
                new Example("Load a default JSON configuration file into Inventor.  The default file name is default.json, and it must reside in the current working directory", new Options { }),
                new Example("Load a JSON configuration file into Inventor", new Options { path = "customConfig.json" }),
                new Example("Write a JSON configuration file from Inventor", new Options { output = "currentConfig.json" }),
                new Example("Verify a JSON configuration file has no errors", new Options { path = "fileToTest.json", test = true })
                };
            }
        }
    }
}
