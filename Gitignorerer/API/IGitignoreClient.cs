using Gitignorerer.Utils;

namespace Gitignorerer.API
{
    public interface IGitignoreClient
    {
        public Task<HashSet<string>> GetTemplateNames();
        public Task<IgnoreSection> GetTemplate(string name);
    }
}
