using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using Xunit;
using Gitignorerer.IO;
using Gitignorerer.Utils;

namespace Gitignorerer.Tests.IO
{
    public class GitignoreWriterTest
    {
        private readonly Mock<IFileWrapper> _mockFileWrapper;
        private readonly Mock<IConsoleWrapper> _mockConsole;
        private readonly GitignoreWriter _writer;

        public GitignoreWriterTest()
        {
            _mockFileWrapper = new Mock<IFileWrapper>();
            _mockConsole = new Mock<IConsoleWrapper>();
            _writer = new GitignoreWriter(_mockFileWrapper.Object, _mockConsole.Object);
        }

        [Fact]
        public async void GitignoreWriter_WhenGitignorePresent_LogsMessage()
        {
            _mockFileWrapper.Setup(mock => mock.Exists()).Returns(true);

            await _writer.WriteToGitignore(Array.Empty<IgnoreSection>());

            _mockConsole.Verify(mock => mock.WriteLine("Found gitignore, writing to file..."), Times.Once);
        }

        [Fact]
        public async void GitignoreWriter_WhenGitignoreNotPresent_LogsMessage()
        {
            _mockFileWrapper.Setup(mock => mock.Exists()).Returns(false);

            await _writer.WriteToGitignore(Array.Empty<IgnoreSection>());

            _mockConsole.Verify(mock => mock.WriteLine("No gitignore found, creating new file..."), Times.Once);
        }
    }
}
