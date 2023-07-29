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
using System.IO;

namespace Gitignorerer.Tests.IO
{
    public class GitignoreWriterTest : IDisposable
    {
        private readonly Mock<IConsoleWrapper> _mockConsole;
        private readonly string _tempFilePath;
        private readonly GitignoreWriter _writer;
        private bool disposedValue;

        public GitignoreWriterTest()
        {
            _mockConsole = new Mock<IConsoleWrapper>();
            _tempFilePath = Path.GetTempFileName();
            _writer = new GitignoreWriter(_mockConsole.Object);
        }

        [Fact]
        public async void GitignoreWriter_WhenGitignorePresent_LogsMessage()
        {
            using (await _writer.OpenGitignore(_tempFilePath))
            {
                _mockConsole.Verify(mock => mock.WriteLine("Found gitignore, writing to file..."), Times.Once);
            }
        }

        [Fact]
        public async void GitignoreWriter_WhenGitignoreNotPresent_LogsMessage()
        {
            File.Delete(_tempFilePath);
            using (await _writer.OpenGitignore(_tempFilePath))
            {
                _mockConsole.Verify(mock => mock.WriteLine("No gitignore found, creating new file..."), Times.Once);
            };   
        }

        [Fact]
        public async void GitignoreWriter_WithValidContent_WritesToFile()
        {
            using var stringWriter = new StringWriter();
            var expected = new IgnoreSection[]
            {
                new IgnoreSection("test1", new string[] { "1", "2", "3" })
            };
            await _writer.WriteToGitignore(expected, stringWriter);
            stringWriter.ToString().Should().Be(string.Join("", expected.AsEnumerable()));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                File.Delete(_tempFilePath);
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
