using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.DTO
{
    public class SaleEntity
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

        public virtual SalePointEntity SalePoint { get; init; }

        public virtual UserEntity User { get; init; }

        public virtual ICollection<SalesDataDto> SalesDatas { get; set; }

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



        public virtual ProductEntity Product { get; init; }

        public virtual SaleEntity Sale { get; init; }
    }
}
