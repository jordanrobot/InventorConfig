using System;
using System.Reflection;

namespace ConfigLoaderConsole
{
    public static class View
    {
        public static void ShowHelp()
        {
            Console.WriteLine("This is a help message... TBD...");
        }

        public static void ShowDryRun()
        {
            Console.WriteLine("This is a dry-run; nothing has been changed in Inventor's configuration.");
        }
        public static void ShowError()
        {
            // Prompt user here
            Console.WriteLine("Could not find config-loader.json in same directory as this application.");
            Console.WriteLine();
            Console.WriteLine("Please run with the --path <path to a configuration JSON file> option, or include a default config-loader.json file in the working directory.");
            Console.WriteLine();
            Console.WriteLine("Press enter to exit...");
        }

        public static void ShowVersion()
        {
            Console.WriteLine("Inventor config-loader v{0}", Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine();
        }

        public static void ShowBanner()
        {
            Console.WriteLine("Inventor config-loader v{0}", Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("Attempting to edit your Autodesk Inventor configuration.  Please stand by...");
        }
    }
}
