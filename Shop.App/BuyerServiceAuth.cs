using Shop.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App
{
    public static class BuyerServiceAuth
    {
        static IBuyerRepository _buyerRepository;

        static BuyerServiceAuth()
        {
            _buyerRepository = new BuyerRepository();
        }

        public static  bool Authenfication(string login, string password)
        {
            return _buyerRepository.AuthenficationBuyer(login, password);
        }


    }
}
