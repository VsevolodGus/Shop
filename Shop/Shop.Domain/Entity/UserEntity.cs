using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.DTO
{
    public class UserEntity
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
