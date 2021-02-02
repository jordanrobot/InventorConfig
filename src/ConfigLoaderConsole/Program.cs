using System;
using ConfigLoader;
using CommandLine;
using CommandLine.Text;

namespace ConfigLoaderConsole
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var results = Parser.Default.ParseArguments<Options>(args);
            results.WithParsed(options => ConsoleStart(options));
        }

        private static void ConsoleStart(Options options)
        {
            ShowBanner();

            ConfigFile configFile = new ConfigFile(options.path);
            if (configFile.FilePath is null)
            {
                ShowNoValidConfigFileError();
                return;
            }

            //modify the Inventor config
            try
            {
                ConfigEngine configEngine = new ConfigEngine(configFile.FilePath, options.test);

                if (options.test)
                {
                    ShowTest();
                } else
                {
                    ShowComplete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //throw new SystemException(e.Message, e);
            }
        }

        #region Screens
        private static void ShowComplete()
        {
            Console.WriteLine("Configuration update complete.");
        }

        private static void ShowTest()
        {
            Console.WriteLine("The json configuration file was valid; nothing was changed in Inventor's configuration.");
        }

        private static void ShowNoValidConfigFileError()
        {
            // Prompt user here
            Console.WriteLine(HeadingInfo.Default);
            Console.WriteLine("Could not find a valid json config file.  Exiting.");
        }

        private static void ShowBanner()
        {
            Console.WriteLine("Attempting to edit your Autodesk Inventor configuration.  Please stand by...");
        }
        #endregion Screens

    }
}