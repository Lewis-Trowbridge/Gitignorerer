using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Gitignorerer.Utils;
using Gitignorerer.API;
using Xunit;
using Moq;
using Moq.Contrib.HttpClient;
using FluentAssertions;

namespace Gitignorerer.Tests.API
{
    public class GithubGitignoreClientTest
    {
        private readonly GithubGitignoreClient _githubGitignoreClient;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _mockHttpClient;
        
        public GithubGitignoreClientTest()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockHttpClient = _mockHttpMessageHandler.CreateClient();

            _githubGitignoreClient = new GithubGitignoreClient(_mockHttpClient);
        }

        [Fact]
        public async void GithubGitignoreClient_WhenGetNamesCalled_CallsCorrectUrl()
        {
            var expectedUrl = "https://api.github.com/gitignore/templates";
            _mockHttpMessageHandler.SetupRequest(expectedUrl).ReturnsResponse(HttpStatusCode.OK, "[]");

            await _githubGitignoreClient.GetTemplateNames();

            _mockHttpMessageHandler.VerifyRequest(expectedUrl);
        }

        [Fact]
        public async void GithubGitignoreClient_WhenGetNamesCalled_ReturnsCorrectList()
        {
            var expectedUrl = "https://api.github.com/gitignore/templates";
            var mockResponse = File.ReadAllText("API/Assets/templates.json");
            string[] expectedResult = { "VisualStudio", "Python", "Kotlin" };
            _mockHttpMessageHandler.SetupRequest(expectedUrl).ReturnsResponse(HttpStatusCode.OK, mockResponse);

            var realResult = await _githubGitignoreClient.GetTemplateNames();

            realResult.Should().BeEquivalentTo(expectedResult);
        }


        [Fact]
        public async void GithubGitignoreClient_WhenGetTemplateCalled_CallsCorrectUrl()
        {
            var expectedUrl = "https://api.github.com/gitignore/templates/test";
            var expectedAcceptHeader = new MediaTypeHeaderValue("application/vnd.github.v3.raw");
            _mockHttpMessageHandler.SetupRequest(expectedUrl).ReturnsResponse(HttpStatusCode.OK, "");

            await _githubGitignoreClient.GetTemplate("test");

            _mockHttpMessageHandler.VerifyRequest(expectedUrl, r => r.Headers.Accept.Contains(expectedAcceptHeader));
        }

        [Fact]
        public async void GithubGitignoreClient_WhenGetTemplateCalled_GetsIgnoreSection()
        {
            var expectedUrl = "https://api.github.com/gitignore/templates/test";
            var mockGitignore = "DEFINITELY\n\nA\n\nGITIGNORE\nFILE";
            var expectedResult = new IgnoreSection("test", new string[] { 
                    $"DEFINITELY",
                    "",
                    $"A",
                    "",
                    $"GITIGNORE",
                    "FILE"
                }
            );
            _mockHttpMessageHandler.SetupRequest(expectedUrl).ReturnsResponse(HttpStatusCode.OK, mockGitignore);

            var realResult = await _githubGitignoreClient.GetTemplate("test");

            realResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
