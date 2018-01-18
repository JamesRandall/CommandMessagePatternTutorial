using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace OnlineStore.Api.Binders
{
    internal static class AuthenticatedUserIdAwareBodyModelBinderProviderInstaller
    {
        public static void AddAuthenticatedUserIdAwareBodyModelBinderProvider(this MvcOptions options)
        {
            IModelBinderProvider bodyModelBinderProvider = options.ModelBinderProviders.Single(x => x is BodyModelBinderProvider);
            int index = options.ModelBinderProviders.IndexOf(bodyModelBinderProvider);
            options.ModelBinderProviders.Remove(bodyModelBinderProvider);
            options.ModelBinderProviders.Insert(index, new AuthenticatedUserIdAwareBodyModelBinderProvider(bodyModelBinderProvider));
        }
    }

    internal class AuthenticatedUserIdAwareBodyModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinderProvider _decoratedProvider;

        public AuthenticatedUserIdAwareBodyModelBinderProvider(IModelBinderProvider decoratedProvider)
        {
            _decoratedProvider = decoratedProvider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            IModelBinder modelBinder = _decoratedProvider.GetBinder(context);
            return modelBinder == null ? null : new AuthenticatedUserIdAwareBodyModelBinder(_decoratedProvider.GetBinder(context));
        }
    }
}
