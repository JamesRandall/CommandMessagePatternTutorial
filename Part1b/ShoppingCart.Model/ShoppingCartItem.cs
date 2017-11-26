using Store.Model;

namespace ShoppingCart.Model
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }

        public StoreProduct Product { get; set; }
    }
}
