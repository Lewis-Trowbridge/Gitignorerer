using System;
using Xunit;
using Moq;
using Gitignorerer.Utils;
using Gitignorerer.API;
using Gitignorerer.IO;
using System.Collections.Generic;
using System.Linq;

namespace Gitignorerer.Tests
{
    public class GitignorererApplicationTest
    {
        private readonly Mock<IConsoleWrapper> mockConsole;

        private readonly Mock<IGitignoreClient> mockGithubGitignoreClient;

        private readonly Mock<IGitignoreWriter> mockGitignoreWriter;

        private readonly GitignorererApplication gitignorererApplication;

        public GitignorererApplicationTest()
        {
            mockConsole = new Mock<IConsoleWrapper>();
            mockGithubGitignoreClient = new Mock<IGitignoreClient>();
            mockGitignoreWriter= new Mock<IGitignoreWriter>();
            gitignorererApplication = new GitignorererApplication(mockConsole.Object, mockGithubGitignoreClient.Object, mockGitignoreWriter.Object);
        }

        [Fact]
        public async void GitignorererApplication_WhenNoIgnoreFilesGiven_LogsAndExits()
        {
            var expectedMessage = "No ignore files given, exiting";

            await gitignorererApplication.Run(null);

            mockConsole.Verify(console => console.WriteLine(expectedMessage), Times.Once());
            mockConsole.VerifyNoOtherCalls();
        }

        [Fact]
        public async void GitignorererApplication_WhenIgnoreFilesGiven_DoesNotLogExitMessage()
        {
            var expectedMessage = "No ignore files given, exiting";
            var mockValidName = "test";
            mockGithubGitignoreClient.Setup(mock => mock.GetTemplateNames()).ReturnsAsync(new HashSet<string>(new string[] { mockValidName }));

            await gitignorererApplication.Run(new HashSet<string>(new string[] { mockValidName }));

            mockConsole.Verify(console => console.WriteLine(expectedMessage), Times.Never);

        }

        [Fact]
        public async void GitignorererApplication_WhenValidIgnoreFileNameGiven_GetsIgnoreSection()
        {
            var mockValidName = "test";
            mockGithubGitignoreClient.Setup(mock => mock.GetTemplateNames()).ReturnsAsync(new HashSet<string>(new string[] { mockValidName }));

            await gitignorererApplication.Run(new HashSet<string>(new string[] { mockValidName }));

            mockGithubGitignoreClient.Verify(mock => mock.GetTemplate(mockValidName));
        }

        [Fact]
        public async void GitignorererApplication_WhenInvalidIgnoreFileNameGiven_LogsErrorMessage()
        {
            var mockInvalidName = "invalid";
            mockGithubGitignoreClient.Setup(mock => mock.GetTemplateNames()).ReturnsAsync(new HashSet<string>());

            await gitignorererApplication.Run(new HashSet<string>(new string[] { mockInvalidName }));

            mockConsole.Verify(mock => mock.WriteLine($"{mockInvalidName} is not a valid file name, skipping..."));
        }
    }
}