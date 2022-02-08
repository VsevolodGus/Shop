using Shop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory
{
    public class SalePointRepository
    {
        private readonly List<SalePointDto> _salePoints;

        public SalePointRepository()
        {
            _salePoints = new List<SalePointDto>()
            {
                // сюда нужно заполнить примерные модели
            };
        }
    }
}
