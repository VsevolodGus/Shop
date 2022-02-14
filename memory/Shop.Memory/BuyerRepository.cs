using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory
{
    public class BuyerRepository : IBuyerRepository
    {
        private List<Buyer> buyers;

        public BuyerRepository()
        {
            buyers = new List<Buyer>();
        }

        public Buyer GetBuyerById(Guid buyerId)
        {
            return buyers.FirstOrDefault(c => c.BuyerId == buyerId);
        }

        public bool AuthenficationBuyer(string login, string password)
        {
            return buyers.Any(c => (c.Email == login || c.NumberPhone == login) && c.Password == password);
        }
    }
}
