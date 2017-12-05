using Core.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineStore.Api.Extensions
{
    public static class CommandResponseExtensions
    {
        public static void AddToModelState(
            this CommandResponse commandResponse,
            ModelStateDictionary modelStateDictionary)
        {
            foreach (CommandError error in commandResponse.Errors)
            {
                modelStateDictionary.AddModelError(error.Key ?? "", error.Message);
            }
        }
    }
}
