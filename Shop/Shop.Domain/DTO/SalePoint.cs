using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.DTO
{
    public class SalePointDto
    {
        [Key]
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Address { get; init; }

        public ICollection<ProvidedProductDto> ProvidedProducts { get; set; }
    }

    public class ProvidedProductDto
    {
        [Key]
        public long PKID { get; init; }

        //[Key, Column(Order = 0)]
        [Required]
        public Guid SalePointId { get; init; }

        //[Key, Column(Order = 1)]
        [Required]
        public Guid ProductId { get; init; }

        public int Count { get; init; }

        public virtual SalePointDto SalePoint { get; init; }

        public virtual ProductDto Product  { get; init; }
    }
}
