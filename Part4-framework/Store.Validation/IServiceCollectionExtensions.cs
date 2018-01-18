using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Store.Commands;

namespace Store.Validation
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IValidator<GetStoreProductQuery>, GetStoreProductQueryValidator>();
            return serviceCollection;
        }
    }
}
