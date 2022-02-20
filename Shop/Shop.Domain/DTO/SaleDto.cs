using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.DTO
{
    public class SaleDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PKID { get; init; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid SalePointId { get; init; }

        public Guid? UserId { get; init; }

        [DefaultValue(false)]
        public bool IsChanled { get; set; }

        public virtual SalePointDto SalePoint { get; init; }

        public virtual UserDto User { get; init; }

        public ICollection<SalesDataDto> SalesDatas { get; set; }

    }


    public class SalesDataDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PKID { get; init; }

        [Required]
        public long SaleId { get; set; }

        [Required]
        public Guid ProductId { get; init; }

        [DefaultValue(0)]
        public long ProductQuantity { get; set; }

        [DefaultValue(0)]
        public decimal ProductPrice { get; set; }



        public virtual ProductDto Product { get; init; }

        public virtual SaleDto Sale { get; init; }
    }
}
