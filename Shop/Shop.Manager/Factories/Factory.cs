using Shop.Domain.DTO;
using System;
using System.Collections.Generic;

namespace Shop.Manager.Factories
{
    internal class Factory
    {
        public  static SaleEntity CreateSale(in Guid? userId, in Guid salePointId, in string comment, in ICollection<SalesDataDto> product)
        {
            return new SaleEntity()
            {
                SalePointId = salePointId,
                Date = DateTime.UtcNow,
                SalesDatas = product,
                UserId = userId,
                Cooment = comment,
                IsChanled = false,
            };
        }
    }

}
