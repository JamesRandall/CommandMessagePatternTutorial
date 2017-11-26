using System;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.DataAccess
{
    public interface IStoreProductRepository
    {
        Task<StoreProduct> GetAsync(Guid productId);
    }
}
