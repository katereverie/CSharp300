namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ConvertToTitleCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var wordArr = input.Split(' ');
            for (int i = 0; i < wordArr.Length; i++)
            {
                wordArr[i] = char.ToUpper(wordArr[i][0]) + wordArr[i].Substring(1).ToLower();
            }

            return $"\"{string.Join(' ', wordArr)}\" is the input in title case.";
        }
    }
}
