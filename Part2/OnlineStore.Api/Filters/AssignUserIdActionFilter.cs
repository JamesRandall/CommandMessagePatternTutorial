using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Api.Extensions;

namespace OnlineStore.Api.Filters
{
    public class AssignUserIdActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (object parameter in context.ActionArguments.Values)
            {
                if (parameter is IUserContextCommand userContextCommand)
                {
                    userContextCommand.UserId = ((Controller) context.Controller).GetUserId();
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
