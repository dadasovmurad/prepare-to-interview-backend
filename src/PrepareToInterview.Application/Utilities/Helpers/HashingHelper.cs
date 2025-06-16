using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Utilities.Helpers
{
    public static class HashingHelper
    {
        /// <summary>
        /// Hashes a plain text using SHA256.
        /// </summary>
        public static byte[] CreateHash(string plainText)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Verifies a plain text against a stored hash.
        /// </summary>
        public static bool VerifyHash(string plainText, byte[] storedHash)
        {
            var computedHash = CreateHash(plainText);
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
