using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.DTO
{
    public class ProductDto
    {
        [Key]
        public Guid ProductId { get; set; }

        [Unique]
        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
