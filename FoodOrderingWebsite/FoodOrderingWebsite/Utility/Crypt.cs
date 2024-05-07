using System.Security.Cryptography;
using System.Text;

namespace FoodOrderingWebsite.Utility
{
    public class Crypt
    {
        public static string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // Method to verify if a string matches a given MD5 hash
        public static bool VerifyMD5Hash(string input, string hash)
        {
            // Hash the input
            string inputHash = ComputeMD5Hash(input);

            // Compare the hashes
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(inputHash, hash) == 0;
        }
    }
}
