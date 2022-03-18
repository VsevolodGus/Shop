using System;
using System.Collections.Generic;

namespace UserUtils
{
    public class AuthUtil
    {
        private readonly static Dictionary<string, Guid> authUsers = new Dictionary<string, Guid>();

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

    }
}
