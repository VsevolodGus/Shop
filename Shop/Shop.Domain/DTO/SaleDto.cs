using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.DTO
{
    public class SaleDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PKID { get; init; }

        // сюда записывать только DateTime.Date
        [Required]
        public DateTime Date { get; init; }

        [Required]
        public Guid SalePointId { get; init; }

        public Guid? UserId { get; init; }

        [DefaultValue(0)]
        public decimal TotalAmount { get; init; }

        public virtual SalePointDto SalePoint {get; init;}

        public virtual UserDto User { get; init; }

        public ICollection<SalesDataDto> SalesDatas { get; init; }
    }


    public class SalesDataDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PKID { get; init; }

        [Required]
        public long SaleId { get; init; }
        
        [Required]
        public Guid ProductId { get; init; }

        [DefaultValue(0)]
        public long ProductQuantity { get; init; }

        [DefaultValue(0)]
        public decimal ProductIdAmount { get; init; }

        public virtual ProductDto Product { get; init; }

        public virtual SaleDto Sale { get; init; }
    }
}
