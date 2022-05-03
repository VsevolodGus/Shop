using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.DTO
{
    public class SalePointDto
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public virtual ICollection<ProvidedProductDto> ProvidedProducts { get; set; }
    }

    public class ProvidedProductDto
    {
        [Key]
        public long PKID { get; init; }


        [Required]
        public Guid SalePointId { get; set; }


        [Required]
        public Guid ProductId { get; init; }

        [DefaultValue(0)]
        public long Count { get; set; }

        public virtual SalePointDto SalePoint { get; init; }

        public virtual ProductDto Product { get; init; }
    }
}
