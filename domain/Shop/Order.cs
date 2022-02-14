using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class Order
    {
        public long PKID { get; set; }

        public Guid? BuyerId { get; set; }

        public string Email { get; set; }

        public string NumberPhone { get; set; }

        public decimal TotalAmount { get; set; }

        public long CountProduct { get; set; }

        public IDictionary<Guid, Product> Products { get; set; } 

        public Guid SalePointId { get; set; }

        public DateTime CreateOrder { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsChanled { get; set; }
    }
}
