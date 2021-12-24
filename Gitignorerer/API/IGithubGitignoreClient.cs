using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitignorerer.Utils;

namespace Gitignorerer.API
{
    public interface IGithubGitignoreClient
    {
        public Task<string[]> GetTemplateNames();
        public Task<IgnoreSection> GetTemplate(string name);
    }
}
