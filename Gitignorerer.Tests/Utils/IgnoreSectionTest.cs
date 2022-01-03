using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Gitignorerer.Utils;

namespace Gitignorerer.Tests.Utils
{
    public class IgnoreSectionTest
    {
        [Fact]
        public void IgnoreSection_WhenConvertedToString_ProducesCorrectResult()
        {
            var testIgnoreSection = new IgnoreSection("Test", new string[] { "Test", "Of", "Result" });
            var expectedResult = "---------------------Test---------------------\nTest\nOf\nResult\n---------------------\n";

            var actualResult = testIgnoreSection.ToString();

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
