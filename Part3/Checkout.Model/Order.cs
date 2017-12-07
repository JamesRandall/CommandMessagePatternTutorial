using System;
using System.Collections.Generic;

namespace Checkout.Model
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public IReadOnlyCollection<OrderItem> OrderItems { get; set; }

        public double PercentageDiscountApplied { get; set; }

        public double Total { get; set; }

        public bool PaymentMade { get; set; }
    }
}
