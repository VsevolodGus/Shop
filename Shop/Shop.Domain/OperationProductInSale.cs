using System;

namespace Shop.Domain
{
    public class OperationProductInSale
    {
        public long SaleId { get; init; }

        public Guid ProductId { get; init; }

        public long Count { get; init; }
    }
}
