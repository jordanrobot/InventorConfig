using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorConfig;
using CommandLine;
using CommandLine.Text;

namespace InventorConfig.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
            var results = Parser.Default.ParseArguments<Options>(args);
            results.WithParsed(options => ConsoleUI.ConsoleStart(options));
        }
    }
}