using Microsoft.AspNetCore.Http;
using Shop.Domain.InterfaceRepository;
using Shop.Domain.Model;
using Shop.Manager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Manager
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected ISession Session => _httpContextAccessor.HttpContext.Session;
        public SaleService(ISaleRepository saleRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._saleRepository = saleRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task AddProductInSale()
        {

        }

        public async Task<bool> RemoveItemFromSale(Guid itemId, long count)
        {
            return false;
        }


        public async Task<bool> RemoveFullProductFromSale(Guid productId)
        {
            return false;
        }

        internal async Task<(bool hasValue, Sale order)> TryGetOrderAsync()
        {
            if (Session.TryGetCart(out Cart cart))
            {
                //var order = await _saleRepository.GetOrderFromCashAsync();

                return (true, null);
            }

            return (false, null);
        }
        public async Task<Sale> GetSaleAsync()
        {
            return null;
        }


        internal async Task<IEnumerable<Product>> GetProductsAsync(Sale sale)
        {
            //    var bookIds = order.Items.Select(item => item.ProductId);

            //    return await productRepository.GetAllByIdsAsync(bookIds);

            return null;
        }

        void UpdateSession(Sale sale)
        {
            var cart = new Cart(sale.SaleId, sale.TotalCount, sale.TotalAmount);
            Session.Set(cart);
        }

    }
}
