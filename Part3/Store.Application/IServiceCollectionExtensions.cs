using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Handlers;
using Store.Application.Repository;
using Store.Validation;

namespace Store.Application
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseStore(this IServiceCollection serviceCollection,
            ICommandRegistry commandRegistry)
        {
            serviceCollection.AddSingleton<IStoreProductRepository, StoreProductRepository>();

            commandRegistry.Register<GetStoreProductQueryHandler>();

            serviceCollection.RegisterValidators();

            return serviceCollection;
        }
    }
}
