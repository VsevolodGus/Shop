using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.InterfaceRepository
{
    public interface ISaleRepository
    {
        Task<List<SaleDto>> GetSales(bool allUsers, Guid? userId, string search, Guid? SalePoinId, int skipCount, int count);


        Task<SaleDto> GetsaleByPKID(long saleId);
    }
}
