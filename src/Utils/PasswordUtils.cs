using System.Security.Cryptography;
using System.Text;

namespace BookStore.src.Utils
{
    public class PasswordUtils
    {
        public static void Password(
            string originalPassword,
            out string hashedPassword,
            out byte[] salt
        )
        {
            var hasher = new HMACSHA256();
            salt = hasher.Key;
            hashedPassword = BitConverter.ToString(
                hasher.ComputeHash(Encoding.UTF8.GetBytes(originalPassword))
            );
        }

        public static bool VerifyPassword(string plainPassword, byte[] salt, string hashedPassword)
        {
            var hasher = new HMACSHA256();
            hasher.Key = salt;
            return BitConverter.ToString(hasher.ComputeHash(Encoding.UTF8.GetBytes(plainPassword)))
                == hashedPassword;
        }
    }
}
