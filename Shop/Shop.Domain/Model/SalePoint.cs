using Shop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Model
{
    public class SalePoint
    {
        private readonly SalePointDto _salePointDto;

        public SalePoint(SalePointDto salePointDto)
        {
            this._salePointDto = salePointDto;
        }
        public Guid Id { get => _salePointDto.Id; }

        public string Name
        {
            get => _salePointDto.Name;
            set => _salePointDto.Name = value;
        }

        public string Address
        {
            get => _salePointDto.Address;
            set => _salePointDto.Address = value;
        }

        public List<ProvidedProductDto> ProvidedProducts
        { 
            get => _salePointDto.ProvidedProducts.Select(c => c).ToList();
        }

        public void AddProuctInAssortiment(Guid prodcutId, long count)
        {
            if (_salePointDto.ProvidedProducts.Any(c => c.ProductId == prodcutId))
            {
                _salePointDto.ProvidedProducts.FirstOrDefault(c => c.ProductId == prodcutId).Count += count;
            }
            else
            {
                _salePointDto.ProvidedProducts.Add(new ProvidedProductDto()
                {
                    ProductId = prodcutId,
                    Count = count,
                    SalePointId = this.Id,
                });

                // добавление в базу
            }
        }


        public void removeProductFromAssortiment(Guid productId, long removeCount)
        {
            if (!_salePointDto.ProvidedProducts.Any(c => c.ProductId == productId && c.Count > 0))
                return;

            var item = _salePointDto.ProvidedProducts.FirstOrDefault(c => c.ProductId == productId);

            if (item.Count > removeCount)
            {
                _salePointDto.ProvidedProducts.FirstOrDefault(c => c.ProductId == productId).Count -= removeCount;
            }
            else
            {
                _salePointDto.ProvidedProducts.Remove(item);
            }
        }


        public static class Mapper
        {
            public static SalePoint Map(SalePointDto dto) => new SalePoint(dto);

            public static SalePointDto Map(SalePoint domain) => domain._salePointDto;
        }




    }
}