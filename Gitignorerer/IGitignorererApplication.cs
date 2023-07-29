using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace Gitignorerer
{
    public interface IGitignorererApplication
    {
        public Task Run(HashSet<string>? ignoreFileNames);
    }
}
