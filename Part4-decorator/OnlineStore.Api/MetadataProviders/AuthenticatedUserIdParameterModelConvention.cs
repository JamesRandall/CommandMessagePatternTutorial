using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace OnlineStore.Api.MetadataProviders
{
    internal static class AuthenticatedUserIdAwareMetaDataProviderInstaller
    {
        public static void AddAuthenticatedUserIdAwareMetaDataProvider(this MvcOptions options)
        {
            IMetadataDetailsProvider underlyingBindingMetadataProvider = options.ModelMetadataDetailsProviders.Single(x => x is DefaultBindingMetadataProvider);
            int index = options.ModelMetadataDetailsProviders.IndexOf(underlyingBindingMetadataProvider);
            options.ModelMetadataDetailsProviders.Remove(underlyingBindingMetadataProvider);
            options.ModelMetadataDetailsProviders.Insert(index, new AuthenticatedUserIdAwareMetaDataProvider((IBindingMetadataProvider)underlyingBindingMetadataProvider));
        }
    }

    public class AuthenticatedUserIdAwareMetaDataProvider : IBindingMetadataProvider
    {
        private readonly IBindingMetadataProvider _decoratedMetadataProvider;

        public AuthenticatedUserIdAwareMetaDataProvider(IBindingMetadataProvider bindingMetadataProvider)
        {
            _decoratedMetadataProvider = bindingMetadataProvider;
        }

        public void CreateBindingMetadata(BindingMetadataProviderContext context)
        {
            _decoratedMetadataProvider.CreateBindingMetadata(context);
            if (context.Key.Name == "AuthenticatedUserId")
            {
                context.BindingMetadata.IsBindingRequired = false;
                context.BindingMetadata.IsBindingAllowed = false;
            }
        }
    }
}
