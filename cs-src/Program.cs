using System;

namespace cs_src
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome.");

            // I could make all the methods static but it's easier to do other things later.
            var argWorker = new ArgumentWorker(args);
            
            if (argWorker.CheckForPathArg())
            {
                // PS C:\Users\David\Documents\GitHub\config-loader\cs-src\bin\Debug> .\cs_src.exe /path "C:\Users\David\Inventor Configs\app_config.json"
                // Welcome.
                //
                // Args length:
                // 2
                //
                // Arg 0 is: /path
                // Arg 1 is: C:\Users\David\Inventor Configs\app_config.json
                //
                // Could not find app_config.json in same directory as this application.
                //
                // Please rerun with /path and the path to the JSON file.
                //
                // Press enter to exit...
                
            } 
            // else if (FoundJson())
            // {
            //     // Since it's an elseif, args will override looking for json locally
            //     
            // }
            else
            {
                argWorker.PromptUser();
            }
        }
    }
}