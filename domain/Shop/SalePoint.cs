using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class SalePoint
    {
        public Guid SalesPointId { get; set; }

        public string Name { get; set; }

        public IDictionary<Guid, long> ProvidedProducts { get; set; } = new Dictionary<Guid, long>();

        public string Address { get; set; }
        public string Description{ get; set; }

        public SalePoint(Guid id, string name, string address, string description )
        {
            this.SalesPointId = id;
            this.Name = name;
            this.Address = address;
            this.Description = description;
        }
    }
}
