using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manager.Models
{
    public class Cart
    {
        public long  SaleId { get; init; }

        public long TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public Cart(long id, long totalCount, decimal totalPrice)
        {
            SaleId = id;
            TotalCount = totalCount;
            TotalPrice = totalPrice;
        }
    }
}
