using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Gitignorerer
{
    public class GitignorererApplication : IGitignorererApplication
    {

        private readonly IConsole _console;

        public GitignorererApplication(IConsole console)
        {
            _console = console;
        }

        public void Run(string[] ignoreFileNames)
        {
            if (ignoreFileNames != null)
            {
                foreach (var ignoreFileName in ignoreFileNames)
                {
                    // Do things
                }
            }
            else
            {
                _console.WriteLine("No ignore files given, exiting");
            }
        }
    }
}
