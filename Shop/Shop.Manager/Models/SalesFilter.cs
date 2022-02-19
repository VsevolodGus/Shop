﻿using System;

namespace Shop.Manager.Models
{
    public class SalesFilter
    {
        public Guid? UserId { get; }

        public bool AllUser { get; }

        public string Search { get; }

        public Guid? SalePointId { get; }
    }
}
