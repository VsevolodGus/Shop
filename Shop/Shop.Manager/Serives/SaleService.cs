using Shop.Domain.InterfaceRepository;
using System;

namespace Shop.Manager
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            this._saleRepository = saleRepository;
        }
    }
}
