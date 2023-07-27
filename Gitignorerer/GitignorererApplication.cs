using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Gitignorerer.Utils;
using Gitignorerer.API;
using Gitignorerer.IO;

namespace Gitignorerer
{
    public class GitignorererApplication : IGitignorererApplication
    {

        private readonly IConsoleWrapper _console;
        private readonly IGitignoreClient _gitignoreClient;
        private readonly IGitignoreWriter _gitignoreWriter;

        public GitignorererApplication(IConsoleWrapper console, IGitignoreClient githubGitignoreClient, IGitignoreWriter gitignoreWriter)
        {
            _console = console;
            _gitignoreClient = githubGitignoreClient;
            _gitignoreWriter = gitignoreWriter;

        }

        public async void Run(string[] ignoreFileNames)
        {
            if (ignoreFileNames != null)
            {
                var validIgnoreFileNames = await _gitignoreClient.GetTemplateNames();
                ignoreFileNames.Where(ignoreFileNames.Contains).ToList();
                foreach (var ignoreFileName in ignoreFileNames)
                {
                    if (validIgnoreFileNames.Contains(ignoreFileName))
                    {
                        var ignoreSection = await _gitignoreClient.GetTemplate(ignoreFileName);
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
