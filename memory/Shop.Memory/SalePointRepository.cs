using Shop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory
{
    public class SalePointRepository : ISalePointRepository
    {
        private readonly List<SalePoint> _salePoints;

        public SalePointRepository()
        {
            _salePoints = new List<SalePoint>()
            {
                // сюда нужно заполнить примерные модели
            };
        }

        public bool IsExistSalePoint(Guid salePointId)
        {
            return _salePoints.Any(c => c.SalesPointId == salePointId);
        }

        public bool UpdateSalePoint(SalePoint model)
        {
            try
            {
                var salePoint = _salePoints.FirstOrDefault(c => c.SalesPointId == model.SalesPointId);

                salePoint = new SalePoint(model.SalesPointId, model.Name, model.Address, model.Description);

                return true;
            }
            catch
            {
                return false;
            }
        }

        bool ISalePointRepository.AddSalePoint(SalePoint model)
        {
            try
            {
                _salePoints.Add(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        SalePoint ISalePointRepository.GetSalePointById(Guid Id)
        {
            return _salePoints.Where(c => c.SalesPointId == Id).FirstOrDefault();
        }


        List<SalePoint> ISalePointRepository.GetSalePointsBySearch(string search)
        {
            return _salePoints.Where(c => c.Name.Contains(search) || c.Address.Contains(search) || c.Description.Contains(search)).ToList();

        }

        bool ISalePointRepository.RemoveSalePointById(Guid Id)
        {
            var salePoint = _salePoints.FirstOrDefault(c => c.SalesPointId == Id);

            if (salePoint == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
