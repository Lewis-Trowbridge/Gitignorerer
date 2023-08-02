using System.Globalization;
using McMaster.Extensions.CommandLineUtils.Abstractions;

namespace Gitignorerer.Parsers
{
    public class StringToHashSetParser : IValueParser<HashSet<string>>
    {
        public Type TargetType => typeof(HashSet<string>);
        private static readonly HashSet<string> _values = new();

        public HashSet<string> Parse(string? argName, string? value, CultureInfo culture)
        {
            if (value is not null) _values.Add(value);
            return _values;
        }

        object? IValueParser.Parse(string? argName, string? value, CultureInfo culture)
        {
            return Parse(argName, value, culture);
        }
    }
}
