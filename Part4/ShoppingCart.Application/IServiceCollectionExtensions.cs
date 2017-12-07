using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Handlers;
using ShoppingCart.Application.Repositories;
using ShoppingCart.Validation;

namespace ShoppingCart.Application
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseShoppingCart(this IServiceCollection serviceCollection,
            ICommandRegistry commandRegistry)
        {
            serviceCollection.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();

            commandRegistry.Register<GetCartQueryHandler>();
            commandRegistry.Register<AddToCartCommandHandler>();

            serviceCollection.RegisterValidators();

            return serviceCollection;
        }
    }
}
