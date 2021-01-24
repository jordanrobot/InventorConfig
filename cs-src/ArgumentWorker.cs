using System;

namespace cs_src
{
    public class ArgumentWorker
    {
        private string[] _args;
        private string _userInputPath;

        public ArgumentWorker(string[] args)
        {
            _args = args;
        }
        
        public bool CheckForPathArg()
        {
            Console.WriteLine("Args length: ");
            Console.WriteLine(_args.Length);

            var argCounter = 0;
            var foundPathArg = false;
            
            foreach (var argument in _args)
            {
                argCounter++;
                
                Console.WriteLine($"Arg { argCounter } is: { argument }");

                if (argument == Configuration.PathArgString)
                {
                    // Found the /path (PathArgString) arg, next arg should be actual path. Save it.
                    _userInputPath = _args[argCounter + 1];

                    return true;
                }
            }

            // If we didn't return true in the above loop
            return false;
        }
        
        public string GetPathFromArguments()
        {
            return _userInputPath;
        }

        public void PromptUser()
        {
            // Prompt user here
            Console.WriteLine("Could not find app_config.json in same directory as this application.");
            Console.WriteLine();
            Console.WriteLine($"Please rerun with { Configuration.PathArgString } and the path to the JSON file.");
            Console.WriteLine();
            Console.WriteLine("Press enter to exit...");

            Console.ReadLine();
        }
    }
}