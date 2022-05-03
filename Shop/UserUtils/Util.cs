using System.Text;
using System.Security.Cryptography;

namespace UserUtils
{
    public static class Util
    {
        public static string CalculateSHA256Hash(string input)
        {
            // step 1, calculate MD5 hash from input

            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha256.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
