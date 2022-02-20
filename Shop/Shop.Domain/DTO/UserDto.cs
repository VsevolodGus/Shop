using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.DTO
{
    public class UserDto
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; init; }

        public string Email { get; set; }

        public string NumberPhone { get; set; }

        public bool IsDeleted { get; set; }
    }
}
