using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Commands;

namespace ShoppingCart.Validation
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IValidator<AddToCartCommand>, AddToCartCommandValidator>();
            return serviceCollection;
        }
    }
}
