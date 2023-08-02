using Gitignorerer.API;
using Gitignorerer.IO;
using Gitignorerer.Utils;

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

        public async Task Run(HashSet<string>? givenIgnoreFileNames)
        {
            if (givenIgnoreFileNames != null)
            {
                var validIgnoreFileNames = await _gitignoreClient.GetTemplateNames();
                // Leaves only found valid names
                validIgnoreFileNames.IntersectWith(givenIgnoreFileNames);

                // Leaves only invalid names
                givenIgnoreFileNames.ExceptWith(validIgnoreFileNames);

                foreach (var invalidName in givenIgnoreFileNames)
                {
                    _console.WriteLine($"{invalidName} is not a valid file name, skipping...");
                }

                using var fileWriter = await _gitignoreWriter.OpenGitignore();
                var validIgnoreSections = await Task.WhenAll(
                    validIgnoreFileNames.Select(async ignoreFileName => await _gitignoreClient.GetTemplate(ignoreFileName)));

                await _gitignoreWriter.WriteToGitignore(validIgnoreSections, fileWriter);
                _console.WriteLine("Written to gitignore!");
            }

            else
            {
                _console.WriteLine("No ignore files given, exiting");
            }
        }
    }
}
