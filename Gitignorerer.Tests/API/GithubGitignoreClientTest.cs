﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
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
    }
}
