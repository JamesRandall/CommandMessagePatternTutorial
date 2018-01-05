using System;

namespace Checkout.Model
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
