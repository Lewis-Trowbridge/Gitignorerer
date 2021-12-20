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
            gitignorererApplication.Run(null);

            mockConsole.Verify(console => console.WriteLine("No ignore files given, exiting"), Times.Once());
            mockConsole.VerifyNoOtherCalls();
        }
    }
}