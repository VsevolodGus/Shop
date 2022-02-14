using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> orders;

        public OrderRepository()
        {
            this.orders = new List<Order>
            {

            };
        }

        public List<Order> GetOrderByBuyer(Guid buyerId)
        {
            return orders.Where(c => c.IsDeleted == false && c.BuyerId == buyerId)
                         .OrderByDescending(c=>c.CreateOrder)
                         .ToList();
        }

        public Order GetOrderById(long PKID)
        {
            return orders.Where(c => c.IsDeleted == false && c.PKID == PKID).FirstOrDefault();

        }

        public List<Order> GetOrdersByEmailOrPhone(string email = "", string numberPhone = "")
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(email))
            {
                return orders.Where(c => c.Email == email && c.NumberPhone == numberPhone)
                             .OrderByDescending(c=>c.CreateOrder)
                             .ToList();
            }
            else if(!string.IsNullOrEmpty(email))
            {
                return orders.Where(c => c.Email == email)
                             .OrderByDescending(c=>c.CreateOrder)
                             .ToList();
            }
            else if (!string.IsNullOrEmpty(numberPhone))
            {
                return orders.Where(c => c.Email == numberPhone)
                             .OrderByDescending(c=>c.CreateOrder)
                             .ToList();
            }

            return new List<Order>();

        }

        public List<Order> GetOrdersUsersByPeriod(Guid buyerId, DateTime BeginDate, DateTime EndDate)
        {
            return orders.Where(c => c.IsDeleted == false && c.BuyerId == buyerId 
                                     && c.CreateOrder >= BeginDate && c.CreateOrder < EndDate)
                         .OrderByDescending(c=> c.CreateOrder)
                         .ToList();
        }

    }
}
