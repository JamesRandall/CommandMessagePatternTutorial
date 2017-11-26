using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Handlers;
using Store.Application.Repository;

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

            return serviceCollection;
        }
    }
}
