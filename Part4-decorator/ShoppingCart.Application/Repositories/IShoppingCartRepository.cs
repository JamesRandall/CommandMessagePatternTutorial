using System;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Repositories
{
    internal interface IShoppingCartRepository
    {
        Task UpdateAsync(Model.ShoppingCart cart);

        Task<Model.ShoppingCart> GetActualOrDefaultAsync(Guid userId);
    }
}
