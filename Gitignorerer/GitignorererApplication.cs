using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Gitignorerer
{
    public class GitignorererApplication
    {
        public static void Run(CommandArgument ignoreFileNames)
        {
            foreach (var ignoreFileName in ignoreFileNames.Values)
            {
                // Do things
            }
        }
    }
}
