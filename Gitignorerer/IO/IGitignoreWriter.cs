using Gitignorerer.Utils;

namespace Gitignorerer.IO
{
    public interface IGitignoreWriter
    {
        Task<TextWriter> OpenGitignore(string path = "./gitignore");
        Task WriteToGitignore(IgnoreSection[] ignoreSections, TextWriter fileWriter);
    }
}