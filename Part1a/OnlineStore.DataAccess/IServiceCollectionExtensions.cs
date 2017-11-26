using Microsoft.Extensions.DependencyInjection;
using OnlineStore.DataAccess.Implementation;

namespace OnlineStore.DataAccess
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseDataAccess(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();
            serviceCollection.AddSingleton<IStoreProductRepository, StoreProductRepository>();
            serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();
            
            return serviceCollection;
        }
    }
}
