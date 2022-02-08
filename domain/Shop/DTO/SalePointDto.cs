using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DTO
{
    public class SalePointDto
    {
        public Guid SalesPointId { get; set; }

        public string Name { get; set; }

        public IDictionary<Guid, long> ProvidedProducts { get; set; } = new Dictionary<Guid, long>();

        public string Addres { get; set; }
    }
}
