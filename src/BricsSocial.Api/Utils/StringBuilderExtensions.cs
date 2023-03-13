using System.Text;

namespace BricsSocial.Api.Utils
{
    public static class StringBuilderExtensions
    {
        private static HashSet<char> _endings = new HashSet<char> { '.', '!' };

        public static StringBuilder AppendDotWithSpace(this StringBuilder sb)
        {
            if (sb.Length == 0)
                return sb;

            if (!_endings.Contains(sb[sb.Length - 1]))
                sb.Append(". ");
            else if (sb[sb.Length - 1] != ' ')
                sb.Append(' ');

            return sb;
        }

        public static StringBuilder AppendDot(this StringBuilder sb)
        {
            if (sb.Length == 0)
                return sb;

            if (!_endings.Contains(sb[sb.Length - 1]))
                sb.Append('.');

            return sb;
        }
    }
}
