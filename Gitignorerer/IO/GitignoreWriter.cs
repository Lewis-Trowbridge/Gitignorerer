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

        public async Task<TextWriter> OpenGitignore(string path = "./gitignore")
        {
            if (File.Exists(path))
            {
                _console.WriteLine("Found gitignore, writing to file...");
                return new StreamWriter(path);
            }
            else
            {
                _console.WriteLine("No gitignore found, creating new file...");
                await File.Create(path).DisposeAsync();
                return new StreamWriter(path);

            }
        }

        public async Task WriteToGitignore(IgnoreSection[] ignoreSections, TextWriter fileWriter)
        {
            ignoreSections.Select(async section => await fileWriter.WriteAsync(section.ToString()));
        }
    }
}
