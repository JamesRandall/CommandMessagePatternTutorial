using System;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.DataAccess
{
    public interface IShoppingCartRepository
    {
        Task UpdateAsync(ShoppingCart cart);

        Task<ShoppingCart> GetAsync(Guid userId);
    }
}
