using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Gitignorerer.Utils;

namespace Gitignorerer.API
{
    public class GithubGitignoreClient : IGithubGitignoreClient
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

        public Task<IgnoreSection> GetTemplateString(string name)
        {
            throw new NotImplementedException();
        }

        private string[] SplitTemplateListIntoStringArray(string templateListString)
        {
            return JsonSerializer.Deserialize<string[]>(templateListString);
        }
    }
}
