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
        /// <summary>
        /// получение данных о заказе заказов
        /// </summary>
        /// <param name="userId"> данные по пользователю, если null то выборка не заказов не авторизованных пользователей</param>
        /// <param name="salePoint"> по точке покупки is null по все точкам покупок</param>
        /// <param name="search"> для строково поиска</param>
        /// <param name="skipCount"> сколько пропускаем</param>
        /// <param name="count"> сколкьо берем</param>
        /// <returns></returns>
        Task<List<SaleDto>> GetSaleByUser(Guid? userId, Guid? salePoint, string search, int skipCount, int count);


        Task<SaleDto> GetsaleByPKID(long saleId);
    }
}
