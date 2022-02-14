using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public interface IBuyerRepository
    {
        bool AuthenficationBuyer(string login, string password);
    }
}
