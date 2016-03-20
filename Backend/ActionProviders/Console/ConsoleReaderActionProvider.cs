using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ActionProviders.Console
{
    class ConsoleReaderActionProvider : ActionProvider
    {

        internal override ServerAction Dequeue()
        {
            string line = System.Console.ReadLine();
            string[] args = line.Split(' ');

            if (args.Length == 0)
                return ServerAction.Empty;

            switch (args[0])
            {
                case "exit":
                    return ServerAction.Exit;
                default:
                    return ServerAction.Empty;
            }
        }
    }
}
