using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Api.Extensions;

namespace OnlineStore.Api.Filters
{
    public class AssignAuthenticatedUserIdActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (object parameter in context.ActionArguments.Values)
            {
                if (parameter is IUserContextCommand userContextCommand)
                {
                    userContextCommand.AuthenticatedUserId = ((Controller) context.Controller).GetUserId();
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
