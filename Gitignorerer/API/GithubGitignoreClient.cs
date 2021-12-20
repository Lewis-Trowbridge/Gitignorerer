using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Gitignorerer.API
{
    public class GithubGitignoreClient
    {
        private readonly HttpClient _client;

        public GithubGitignoreClient(HttpClient httpClient)
        {
            _client = httpClient;
            httpClient.BaseAddress = new Uri("https://api.github.com");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3.raw"));
        }

        public async Task<string[]> GetTemplateNames()
        {
            var response = await _client.GetAsync("/gitignore/templates");
            var templateString = await response.Content.ReadAsStringAsync();
            return SplitTemplateListIntoStringArray(templateString);
        }

        private string[] SplitTemplateListIntoStringArray(string templateListString)
        {
            return templateListString
                .Replace("[", "")
                .Replace("]", "")
                .Split(Environment.NewLine);
        }
    }
}
