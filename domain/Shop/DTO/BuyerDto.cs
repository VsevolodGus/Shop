using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DTO
{
    public class BuyerDto
    {
        public Guid BuyerId { get; set; }

        public string Name { get; set; }

        public IList<Guid> SalesId { get; set; } = new List<Guid>();

        public string Email { get; set; }

        public string NumberPhone { get; set; }
    }
}
