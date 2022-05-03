using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Domain.Model
{
    public class Sale
    {
        private readonly SaleEntity _saleDto;

        public Sale(SaleEntity saleDto)
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

        public decimal TotalAmount { get => _saleDto.SalesDatas.Sum(c => c.ProductPrice * c.ProductQuantity); }

        public long TotalCount { get => _saleDto.SalesDatas.Sum(c => c.ProductQuantity); }

        public bool IsCancel
        {
            get => _saleDto.IsChanled;
            set => _saleDto.IsChanled = value;
        }

        public SalePoint SalePoint
        {
            get => SalePoint.Mapper.Map(_saleDto.SalePoint);
            init => SalePoint.Mapper.Map(value);
        }

        public List<SalesDataDto> SaleDatas { get => _saleDto.SalesDatas.ToList(); }



        public static class Mapper
        {
            public static Sale Map(SaleEntity dto) => new Sale(dto);

            public static SaleEntity Map(Sale domain) => domain._saleDto;
        }


        public void FillSale(Guid productId, long count, decimal price)
        {
            var saleProduct = _saleDto.SalesDatas.FirstOrDefault(c => c.ProductId == productId);

            if (saleProduct is null)
            {
                var newProductSale = new SalesDataDto()
                {
                    SaleId = _saleDto.PKID,
                    ProductId = productId,
                    ProductPrice = price,
                    ProductQuantity = count,
                };
                _saleDto.SalesDatas.Add(newProductSale);
            }
            else
            {
                saleProduct.ProductQuantity += count;
            }
        }

        public void RemoveItem(Guid productId, long count, bool fullItems = false)
        {
            var saleProduct = _saleDto.SalesDatas.FirstOrDefault(c => c.ProductId == productId);

            if (saleProduct is null)
                return;

            if (saleProduct.ProductQuantity <= count || fullItems)
                _saleDto.SalesDatas.Remove(saleProduct);
            else
                saleProduct.ProductQuantity -= count;
        }
    }
}
