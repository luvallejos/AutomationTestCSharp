using System.Text.RegularExpressions;


namespace UITestFramework.Utilities
{
    public static class StringManipulationHelper
    {
        public static string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var parts = s.Split(' ');

            for (int i = 0; i < parts.Length; i++)
            {
                var word = parts[i];

                if (string.IsNullOrWhiteSpace(word))
                    continue;

                // Capitaliza each word
                parts[i] = char.ToUpper(word[0]) +
                            word.Substring(1).ToLower();
            }

            return string.Join(" ", parts);
        }

        private static string RemoveSpecialCharacters(string text)
        {
            text = Regex.Replace(text, @"\r", "");
            text = Regex.Replace(text, @"\n", "");
            text = Regex.Replace(text, @"\t", "");
            text = Regex.Replace(text, @"\(", "");
            text = Regex.Replace(text, @"\)", "");
            text = Regex.Replace(text, @"\u00A0", "");
            text = Regex.Replace(text, @"<!--.*?-->", "");
            return text;
        }
        public static string ToPlainText(this string text)
        {
            text = RemoveSpecialCharacters(text);
            text = RemoveNumbers(text);
            text = text.Trim();
            return text;
        }

        public static string RemoveNumbers(string text)
        {
            text = Regex.Replace(text, @"[\d-]", "");
            return text;
        }

        public static string UnescapeTextAndRemoveSpaces(this string text)
        {
            text = RemoveSpecialCharacters(text);
            text = Regex.Replace(text, " ", "");
            return text;
        }

    }
}
