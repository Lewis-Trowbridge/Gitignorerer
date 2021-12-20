using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gitignorerer
{
    public interface IGitignorererApplication
    {
        public void Run(string[] ignoreFileNames);
    }
}
