using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Model
{
    public class ShoppingBasket
    {
        public Guid UserId { get; set; }

        public IReadOnlyCollection<ShoppingBasketItem> Items { get; set; }
    }
}
