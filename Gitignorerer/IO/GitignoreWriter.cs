using Gitignorerer.Utils;

namespace Gitignorerer.IO
{
    public class GitignoreWriter : IGitignoreWriter
    {
        private readonly IConsoleWrapper _console;

        public GitignoreWriter(IConsoleWrapper console)
        {
            _console = console;
        }

        public async Task WriteToGitignore(IgnoreSection[] ignoreSections, TextWriter fileWriter, string path = "./gitignore")
        {
            if (File.Exists(path))
            {
                _console.WriteLine("Found gitignore, writing to file...");
                ignoreSections.Select(async section => await fileWriter.WriteAsync(section.ToString()));
            }
            else
            {
                _console.WriteLine("No gitignore found, creating new file...");
            }
        }
    }
}
