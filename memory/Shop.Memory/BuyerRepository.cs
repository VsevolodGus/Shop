using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory
{
    public class BuyerRepository
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


    }
}
