using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gitignorerer.Utils;

namespace Gitignorerer.API
{
    public class GithubGitignoreClient : IGitignoreClient
    {
        private readonly HttpClient _client;

        public GithubGitignoreClient(HttpClient httpClient)
        {
            _client = httpClient;
            httpClient.BaseAddress = new Uri("https://api.github.com");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3.raw"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Gitignorerer")));
        }

        public async Task<HashSet<string>> GetTemplateNames()
        {
            var response = await _client.GetAsync("/gitignore/templates");
            return await response.Content.ReadFromJsonAsync<HashSet<string>>() ?? new HashSet<string>();
        }

        public async Task<IgnoreSection> GetTemplate(string name)
        {
            var response = await _client.GetAsync($"/gitignore/templates/{name}");
            return new IgnoreSection(name, (await response.Content.ReadAsStringAsync()).Split("\n"));
        }
    }
}
