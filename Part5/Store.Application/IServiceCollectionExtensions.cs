using System;
using System.Net.Http;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Http;
using Core.Model;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Handlers;
using Store.Application.Repository;
using Store.Commands;
using Store.Model;
using Store.Validation;

namespace Store.Application
{
    public enum ApplicationModeEnum
    {
        InProcess,
        Client,
        Server
    }

    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseStore(this IServiceCollection serviceCollection,
            Func<IServiceProvider> serviceProvider,
            ICommandRegistry commandRegistry, ApplicationModeEnum applicationMode=ApplicationModeEnum.InProcess)
        {
            serviceCollection.AddSingleton<IStoreProductRepository, StoreProductRepository>();
            if (applicationMode == ApplicationModeEnum.InProcess || applicationMode == ApplicationModeEnum.Server)
            {
                commandRegistry.Register<GetStoreProductQueryHandler>();
            }
            else if (applicationMode == ApplicationModeEnum.Client)
            {
                // this configures the command dispatcher to send the command over HTTP and wait for the result
                Uri functionUri = new Uri("http://localhost:7071/api/GetStoreProduct");
                commandRegistry.Register<GetStoreProductQuery, CommandResponse<StoreProduct>>(() =>
                {
                    IHttpCommandDispatcherFactory httpCommandDispatcherFactory = serviceProvider().GetService<IHttpCommandDispatcherFactory>();
                    return httpCommandDispatcherFactory.Create(functionUri, HttpMethod.Get);
                });
            }
            
            serviceCollection.RegisterValidators();

            return serviceCollection;
        }
    }
}
