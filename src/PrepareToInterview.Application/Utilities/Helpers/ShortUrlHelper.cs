using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Utilities.Helpers
{
    public class ShortUrlHelper
    {
        private static readonly char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static readonly char[] chars = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private static readonly Random random = new Random();

        public static string GenerateShortUrl()
        {
            char[] shortUrl = new char[4];

            // Ensure first 3 characters are letters
            for (int i = 0; i < 3; i++)
            {
                shortUrl[i] = letters[random.Next(letters.Length)];
            }

            // The last character can be letter or digit
            shortUrl[3] = chars[random.Next(chars.Length)];

            // Optionally shuffle the array to make the position of digits unpredictable
            return new string(shortUrl.OrderBy(_ => random.Next()).ToArray());
        }
    }
}
