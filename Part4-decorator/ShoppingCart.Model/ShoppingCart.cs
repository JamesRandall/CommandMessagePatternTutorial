using System;
using System.Collections.Generic;

namespace ShoppingCart.Model
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public Guid UserId { get; set; }

        public IReadOnlyCollection<ShoppingCartItem> Items { get; set; }
    }
}
