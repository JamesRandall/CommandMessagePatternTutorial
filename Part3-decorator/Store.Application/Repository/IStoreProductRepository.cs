using System;
using System.Threading.Tasks;
using Store.Model;

namespace Store.Application.Repository
{
    internal interface IStoreProductRepository
    {
        Task<StoreProduct> GetAsync(Guid productId);
    }
}
