using System;
using System.Threading.Tasks;
using OnlineStore.DataAccess;

namespace OnlineStore.Services.Implementation
{
    class ProductService : IProductService
    {
        private readonly IStoreProductRepository _storeProductRepository;

        public ProductService(IStoreProductRepository storeProductRepository)
        {
            _storeProductRepository = storeProductRepository;
        }

        public Task<Model.StoreProduct> GetAsync(Guid productId)
        {
            return _storeProductRepository.GetAsync(productId);
        }
    }
}
