using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model
{
    public class Sale
    {
        private readonly SaleDto _saleDto;

        public Sale(SaleDto saleDto)
        {
            this._saleDto = saleDto;            
        }

        public DateTime Date
        {
            get => _saleDto.Date;
            set
            {
                var oldDate = _saleDto.Date.TimeOfDay;
                _saleDto.Date = value - oldDate;
            }
        }

        
        public TimeSpan Time
        {
            get => _saleDto.Date.TimeOfDay;
            set
            {
                _saleDto.Date = _saleDto.Date.Date + value;
            }
        }

        public long SaleId { get => _saleDto.PKID; }

        public decimal TotalAmount { get => _saleDto.SalesDatas.Sum(c => c.ProductIdAmount); }

        public long TotalCount { get => _saleDto.SalesDatas.Sum(c => c.ProductQuantity); }

        public SalePoint SalePoint { get; init; }


        public static class Mapper
        {
            public static Sale Map(SaleDto dto) => new Sale(dto);

            public static SaleDto Map(Sale domain) => domain._saleDto;
        }
    }
}
