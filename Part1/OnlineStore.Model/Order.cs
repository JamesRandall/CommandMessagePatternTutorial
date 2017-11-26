using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Model
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
