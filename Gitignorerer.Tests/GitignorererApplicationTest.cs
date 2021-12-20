using McMaster.Extensions.CommandLineUtils;
using Xunit;
using Moq;
using Gitignorerer.Utils;

namespace Gitignorerer.Tests
{
    public class GitignorererApplicationTest
    {
        private readonly Mock<IConsoleWrapper> mockConsole;

        private readonly GitignorererApplication gitignorererApplication;

        public GitignorererApplicationTest()
        {
            mockConsole = new Mock<IConsoleWrapper>();
            gitignorererApplication = new GitignorererApplication(mockConsole.Object);
        }

        [Fact]
        public void GitignorererApplication_WhenNoIgnoreFilesGiven_LogsAndExits()
        {
            var expectedMessage = "No ignore files given, exiting";

            gitignorererApplication.Run(null);

            mockConsole.Verify(console => console.WriteLine(expectedMessage), Times.Once());
            mockConsole.VerifyNoOtherCalls();
        }

        [Fact]
        public void GitignorererApplication_WhenIgnoreFilesGiven_DoesNotLogExitMessage()
        {
            var expectedMessage = "No ignore files given, exiting";

            gitignorererApplication.Run(new string[] { "test" });

            mockConsole.Verify(console => console.WriteLine(expectedMessage), Times.Never);

        }
    }
}