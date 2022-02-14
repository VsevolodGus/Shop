using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.App
{
    public class OrderRepository
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ISalePointRepository _salePointRepository;

        public OrderRepository(IOrderRepository orderRepository,
                               IProductRepository productRepository,
                               ISalePointRepository salePointRepository)
        {
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
            this._salePointRepository = salePointRepository;
        }

        public List<Order> GetOrderByBuyer(Guid buyerId)
        {
            return _orderRepository.GetOrderByBuyer(buyerId);
        }

        public List<Order> GetOrderByEmailOrPhoneNumber(string email = "", string phone ="")
        {
            return _orderRepository.GetOrdersByEmailOrPhone(email, phone);
        }
    }
}
