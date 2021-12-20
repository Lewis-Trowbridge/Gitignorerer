using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Gitignorerer.Utils
{
    public class ConsoleWrapper : IConsoleWrapper
    {

        private readonly IConsole _console;

        public ConsoleWrapper(IConsole console)
        {
            _console = console;
        }

        public void WriteLine(string output)
        {
            _console.WriteLine(output);
        }
    }
}
