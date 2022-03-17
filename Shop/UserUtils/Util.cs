using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UserUtils
{
    public static class Util
    {
        private readonly static Dictionary<string, Guid> authUsers= new Dictionary<string, Guid>();

        public static bool AddUserAuth(string auth, Guid userId)
        {
            if (authUsers.ContainsKey(auth))
                return false;

            authUsers.Add(auth, userId);
            return true;
        }

        public static bool IsAuthUser(string auth, out Guid userId)
        {
            if (authUsers.ContainsKey(auth))
            {
                userId = authUsers[auth];
                return true;
            }
            else
            {
                userId = Guid.Empty;
                return false;
            }

        }

        public static bool LogOutUser(string token)
        {
            if (!authUsers.ContainsKey(token))
                return false;

            authUsers.Remove(token);
            return true;
        }

        public static string CalculateSHA256Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            SHA256 md5 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

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
