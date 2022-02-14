using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    //publiclass Buyer
    //{
    //}

    public class Buyer
    {
        public Guid BuyerId { get; set; }

        public string Name { get; set; }

        //public IList<Guid> SalesId { get; set; } = new List<Guid>();

        public string Email { get; set; }

        public string NumberPhone { get; set; }

        public string Password { get; set; }
    }
}
