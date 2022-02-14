using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public interface IOrderRepository
    {
        public Order GetOrderById(long PKID);

        public List<Order> GetOrdersByEmailOrPhone(string email = "", string numberPhone = "");

        public List<Order> GetOrderByBuyer(Guid buyerId);

        public List<Order> GetOrdersUsersByPeriod(Guid buerId, DateTime BeginDate, DateTime EndDate);
    }
}
