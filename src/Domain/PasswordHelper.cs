using System;
using System.Security.Cryptography;
using System.Text;

namespace FoodOnline.Domain
{
    /// <summary>
    /// Static class to help with the creation and verification of a password, password hash and salt.
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Create a password hash using the <see cref="HMACSHA512"/> class. The password is
        /// converted from byte[] to string using <see cref="Convert.ToBase64String(byte[])"/>
        /// </summary>
        /// <param name="password">The password the hash will be made for.</param>
        /// <param name="hash">The generated hash.</param>
        /// <param name="salt">The generated salt.</param>
        public static void CreatePasswordHash(string password, out string hash, out string salt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using var hmac = new HMACSHA512();
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            salt = Convert.ToBase64String(hmac.Key);
            hash = Convert.ToBase64String(computedHash);
        }

        /// <summary>
        /// Verify if a passward maches the provided hash and salt using the
        /// <see cref="HMACSHA512"/> class. The password will be converted from string to byte[]
        /// using <see cref="Convert.FromBase64String(string)"/>
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="passwordHash">A hash that should match this password.</param>
        /// <param name="passwordSalt">A salt that should match this password.</param>
        /// <returns>True if this password was used to generate the hash and salt otherwise false.</returns>
        public static bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            var hash = Convert.FromBase64String(passwordHash);
            var salt = Convert.FromBase64String(passwordSalt);

            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (hash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(passwordHash));
            if (salt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(passwordHash));

            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != hash[i])
                    return false;
            }
            return true;
        }
    }
}