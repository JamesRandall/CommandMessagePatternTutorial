using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Application.Handlers;
using Checkout.Application.Repositories;
using Checkout.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseCheckout(this IServiceCollection serviceCollection,
            ICommandRegistry registry)
        {
            serviceCollection.AddSingleton<IOrderRepository, OrderRepository>();

            registry.Register<CreateOrderCommandHandler>();
            registry.Register<MakePaymentCommandHandler>();

            serviceCollection.RegisterValidators();

            return serviceCollection;
        }
    }
}
