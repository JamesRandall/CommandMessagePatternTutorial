using System;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static Guid GetUserId(this Controller controller)
        {
            // in reality this would pull the user ID from the claims e.g.
            //     return Guid.Parse(controller.User.FindFirst("userId").Value);
            return Guid.Parse("A9F7EE3A-CB0D-4056-9DB5-AD1CB07D3093");
        }
    }
}
