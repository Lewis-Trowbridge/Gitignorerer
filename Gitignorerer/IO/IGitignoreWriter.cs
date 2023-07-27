using Gitignorerer.Utils;

namespace Gitignorerer.IO
{
    public interface IGitignoreWriter
    {
        Task WriteToGitignore(IgnoreSection[] ignoreSections, TextWriter fileWriter, string path);
    }
}