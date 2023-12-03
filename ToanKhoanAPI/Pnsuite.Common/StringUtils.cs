using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace vn.com.pnsuite.common
{
    public class StringUtils
    {
        public static string CreateHash()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
        public static string Encrypt(string value, string pass)
        {
            var combinedPassword = String.Concat(value, pass);

            var sha512 = new SHA512Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha512.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static string Encrypt(string value)
        {
            var combinedPassword = value;

            var sha512 = new SHA512Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha512.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
