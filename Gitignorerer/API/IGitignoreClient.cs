using Gitignorerer.Utils;

namespace Gitignorerer.API
{
    public interface IGitignoreClient
    {
        public Task<string[]> GetTemplateNames();
        public Task<IgnoreSection> GetTemplate(string name);
    }
}
