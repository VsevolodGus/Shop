using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.DTO
{
    public class ProductDto
    {
        [Key]
        public Guid ProductId { get; init; }

        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
