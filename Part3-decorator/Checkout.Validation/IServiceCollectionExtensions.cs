using Checkout.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Validation
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IValidator<MakePaymentCommand>, MakePaymentCommandValidator>();
            return serviceCollection;
        }
    }
}
