using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manager
{
    public class SalePointService
    {
        private readonly ISalePointRepository _salePointRepository;

        public SalePointService(ISalePointRepository salePointRepository)
        {
            this._salePointRepository = salePointRepository;
        }
    }
}
