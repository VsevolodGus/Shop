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
        public Dictionary<Guid, long> Products { get; set; } = new Dictionary<Guid, long>();

        public Guid? SalePointId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
