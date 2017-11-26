using System;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public interface IShoppingCartService
    {
        Task AddToBasketAsync(Guid userId, Guid productId, int quantity);

        Task<Model.ShoppingCart> GetAsync(Guid userId);

        Task ClearAsync(Guid userId);
    }
}
