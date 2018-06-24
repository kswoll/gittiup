namespace Gittiup.Utils
{
    public static class StringExtensions
    {
        public static string ChopStart(this string s, string prefix)
        {
            if (s.StartsWith(prefix))
            {
                s = s.Substring(prefix.Length);
            }

            return s;
        }
    }
}