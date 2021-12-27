using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Gitignorerer.Utils;

namespace Gitignorerer.IO
{
    public class GitignoreWriter
    {
        private readonly IFileWrapper _fileWrapper;
        private readonly IConsoleWrapper _console;

        public GitignoreWriter(IFileWrapper fileWrapper, IConsoleWrapper console)
        {
            _fileWrapper = fileWrapper;
            _console = console;
        }

        public async Task WriteToGitignore(IgnoreSection[] ignoreSections)
        {
            if (_fileWrapper.Exists())
            {
                _console.WriteLine("Found gitignore, writing to file...");
            }
            else
            {
                _console.WriteLine("No gitignore found, creating new file...");
            }
        }
    }
}
