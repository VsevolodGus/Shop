using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory
{
    public class CheckAuth
    {
        private static List<User> users = new List<User>()
        {

        };


        static bool CheckAuthToken(string token, out Guid userId)
        {
            userId = Guid.Empty;

            return false;
        }
    }
}
