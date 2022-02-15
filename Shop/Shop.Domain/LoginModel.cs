using System.ComponentModel.DataAnnotations;

namespace Shop.Domain
{
    public class LoginModel
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
    }

}
