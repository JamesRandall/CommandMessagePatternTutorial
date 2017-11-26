using System;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public interface IProductService
    {
        Task<Model.StoreProduct> GetAsync(Guid productId);
    }
}
