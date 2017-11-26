using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.DataAccess.Implementation
{
    internal class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly Dictionary<Guid, ShoppingCart> _baskets = new Dictionary<Guid, ShoppingCart>();

        public Task UpdateAsync(ShoppingCart cart)
        {
            _baskets[cart.UserId] = cart;
            return Task.FromResult(0);
        }

        public Task<ShoppingCart> GetAsync(Guid userId)
        {
            _baskets.TryGetValue(userId, out ShoppingCart basket);
            return Task.FromResult(basket);
        }
    }
}
