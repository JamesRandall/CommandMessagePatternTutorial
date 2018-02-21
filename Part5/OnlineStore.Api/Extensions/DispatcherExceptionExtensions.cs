using Core.Commanding.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineStore.Api.Extensions
{
    public static class DispatcherExceptionExtensions
    {
        public static void AddToModelState(this DispatcherException ex, ModelStateDictionary modelStateDictionary)
        {
            modelStateDictionary.AddModelError("", ex.Message);
        }
    }
}
