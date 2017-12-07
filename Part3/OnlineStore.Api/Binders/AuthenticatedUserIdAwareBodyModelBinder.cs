using System;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineStore.Api.Binders
{
    public class AuthenticatedUserIdAwareBodyModelBinder : IModelBinder
    {
        private readonly IModelBinder _decoratedBinder;

        public AuthenticatedUserIdAwareBodyModelBinder(IModelBinder decoratedBinder)
        {
            _decoratedBinder = decoratedBinder;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            await _decoratedBinder.BindModelAsync(bindingContext);
            if (bindingContext.Result.Model is IUserContextCommand command)
            {
                command.AuthenticatedUserId = Guid.Empty;
            }
        }
    }
}
