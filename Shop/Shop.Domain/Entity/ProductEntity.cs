using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.DTO
{
    public class ProductEntity
    {
        [Key]
        public Guid ProductId { get; init; }


        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
