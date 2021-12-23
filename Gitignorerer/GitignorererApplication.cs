using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Gitignorerer.Utils;
using Gitignorerer.API;

namespace Gitignorerer
{
    public class GitignorererApplication : IGitignorererApplication
    {

        private readonly IConsoleWrapper _console;
        private readonly 

        public GitignorererApplication(IConsoleWrapper console,)
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
