using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop
{
    public interface ISalePointRepository
    {
        public SalePoint GetSalePointById (Guid Id);

        public List<SalePoint> GetSalePointsBySearch (string search);

        public bool AddSalePoint (SalePoint model);

        public bool UpdateSalePoint(SalePoint model);

        public bool RemoveSalePointById (Guid Id);

        public bool IsExistSalePoint (Guid salePointId);
    }
}
