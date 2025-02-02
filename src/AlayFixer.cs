using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace src {
    public static class AlayFixer
    {
        // Fix alay using regex
        public static string FixAlayText(string text, List<string> correctNames)
        {
            Dictionary<char, char> numberSubs = new Dictionary<char, char>
            {
                {'1', 'i'}, {'4', 'a'}, {'6', 'g'}, {'0', 'o'}, {'3', 'e'}, {'7', 't'}, {'8', 'b'}, {'5', 's'}, {'9', 'p'}, {'2', 'z'}
            };

            string fixedText = Regex.Replace(text, "[143678059]", match => numberSubs[match.Value[0]].ToString());
            fixedText = fixedText.ToLower();

            string ClosestMatch(string input, List<string> names)
            {
                var similarities = names.Select(name => new
                {
                    Name = name,
                    Similarity = Levenshtein.CalculateLevenshteinSimilarity(input, name.ToLower())
                }).ToList();

                var closest = similarities.OrderByDescending(x => x.Similarity).First();
                return closest.Name;
            }

            string correctedText = ClosestMatch(fixedText, correctNames);

            return correctedText;
        }
    }
}