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
        private readonly IGithubGitignoreClient _githubGitignoreClient;

        public GitignorererApplication(IConsoleWrapper console, IGithubGitignoreClient githubGitignoreClient )
        {
            _console = console;
            _githubGitignoreClient = githubGitignoreClient;
        }

        public async void Run(string[] ignoreFileNames)
        {
            if (ignoreFileNames != null)
            {
                var validIgnoreFileNames = await _githubGitignoreClient.GetTemplateNames();
                foreach (var ignoreFileName in ignoreFileNames)
                {
                    if (validIgnoreFileNames.Contains(ignoreFileName))
                    {
                        var ignoreSection = await _githubGitignoreClient.GetTemplate(ignoreFileName);
                        // Write ignore section to file
                    }
                    else
                    {
                        _console.WriteLine($"{ignoreFileName} is not a valid file name, skipping...");
                    }
                }
            }
            else
            {
                _console.WriteLine("No ignore files given, exiting");
            }
        }
    }
}
