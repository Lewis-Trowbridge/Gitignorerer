using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gitignorerer.Utils
{
    public record IgnoreSection
    {
        public string Name { get; }
        public string[] IgnoreLines { get; }

        public IgnoreSection(string name, string[] ignoreLines)
        {
            Name = name;
            IgnoreLines = ignoreLines;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("---------------------")
                .Append(Name)
                .Append("---------------------")
                .Append('\n')
                .AppendJoin('\n', IgnoreLines)
                .Append('\n')
                .Append("---------------------")
                .Append('\n')
                .ToString();
        }
    }
}
