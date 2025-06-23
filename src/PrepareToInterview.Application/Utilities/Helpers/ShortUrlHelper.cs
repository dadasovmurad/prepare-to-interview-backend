using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Utilities.Helpers
{
    public class ShortUrlHelper
    {
        private static readonly char[] chars = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private static readonly Random random = new Random();

        public static string GenerateShortUrl()
        {
            char[] shortUrl = new char[4];
            for (int i = 0; i < shortUrl.Length; i++)
            {
                shortUrl[i] = chars[random.Next(chars.Length)];
            }
            return new string(shortUrl);
        }
    }
}
