using System;
using System.Threading.Tasks;
using Store.Model;

namespace Store.Application.Repository
{
    internal class StoreProductRepository : IStoreProductRepository
    {
        public Task<StoreProduct> GetAsync(Guid productId)
        {
            return Task.FromResult(new StoreProduct
            {
                Id = productId,
                Name = "A product",
                Price = new Random().Next(200,1000) / 100.0
            });
        }
    }
}
