using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DTO
{

    public class SalesData
    {
        public Guid ProductId { get; set; }
        public long ProductQuantity {get;set;}
        public decimal ProductIdAmount {get;set;}
    }

    public class Sale
    {
        public long SaleId { get; set; }

        public DateTime Date { get; set; }

        public Guid SalesPointId { get; set; }

        public Guid BuyerId { get; set; }

        public IList<SalesData> SalesData { get; set; } = new List<SalesData>();
    }
}
