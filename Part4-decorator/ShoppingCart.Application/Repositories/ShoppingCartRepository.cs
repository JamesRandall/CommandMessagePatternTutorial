using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Repositories
{
    internal class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly Dictionary<Guid, Model.ShoppingCart> _baskets = new Dictionary<Guid, Model.ShoppingCart>();

        public Task UpdateAsync(Model.ShoppingCart cart)
        {
            _baskets[cart.UserId] = cart;
            return Task.FromResult(0);
        }

        public Task<Model.ShoppingCart> GetActualOrDefaultAsync(Guid userId)
        {
            if (!_baskets.TryGetValue(userId, out Model.ShoppingCart cart))
            {
                cart = new Model.ShoppingCart
                {
                    UserId = userId
                };
            }
            return Task.FromResult(cart);
        }
    }
}
