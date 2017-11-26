using System;
using System.Threading.Tasks;
using ShoppingCart.Model;

namespace ShoppingCart.Services
{
    public interface IShoppingBasketService
    {
        Task AddToBasket(Guid userId, Guid productId, int quantity);

        Task<ShoppingBasket> Get(Guid userId);

        Task Clear(Guid userId);
    }
}
