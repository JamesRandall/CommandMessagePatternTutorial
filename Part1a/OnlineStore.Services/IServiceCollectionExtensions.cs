using Microsoft.Extensions.DependencyInjection;
using OnlineStore.DataAccess;
using OnlineStore.Services.Implementation;

namespace OnlineStore.Services
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.UseDataAccess();
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IShoppingCartService, ShoppingCartService>();
            serviceCollection.AddTransient<ICheckoutService, CheckoutService>();
            return serviceCollection;
        }
    }
}
