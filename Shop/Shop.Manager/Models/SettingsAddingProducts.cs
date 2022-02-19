using System;
using System.Collections.Generic;

namespace Shop.Manager.Models
{
    public class SettingsAddingProducts
    {
        /// <summary>
        /// Key- Id продукта
        /// Value - добавляемое кол-во
        /// </summary>
        public Dictionary<Guid, long> Products = new Dictionary<Guid, long>();

        public Guid? SalePointId;
    }
}
