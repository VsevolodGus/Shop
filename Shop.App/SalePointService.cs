using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App
{
    public class SalePointService
    {
        ISalePointRepository _salePointRepository;

        public SalePointService(ISalePointRepository salePointRepository)
        {
            this._salePointRepository = salePointRepository;
        }

        public SalePoint GetSalePointId(Guid salePointId)
        {
            return _salePointRepository.GetSalePointById(salePointId);
        }

        public List<SalePoint> GetSalePointBySearch(string search)
        {
            return _salePointRepository.GetSalePointsBySearch(search);
        }

        public bool SetSalePoint(in SalePoint model)
        {
            if (_salePointRepository.IsExistSalePoint(model.SalesPointId))
            {
                return _salePointRepository.UpdateSalePoint(model);
            }
            else
            {
                return _salePointRepository.AddSalePoint(model);
            }
        }

        public bool RemoveProductById(Guid Id)
        {
            return _salePointRepository.RemoveSalePointById(Id);
        }

    }
}
