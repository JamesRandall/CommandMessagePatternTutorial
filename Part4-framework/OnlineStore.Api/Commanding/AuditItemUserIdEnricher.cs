using System.Collections.Generic;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;

namespace OnlineStore.Api.Commanding
{
    public class AuditItemUserIdEnricher : IAuditItemEnricher
    {
        public void Enrich(Dictionary<string, string> properties, ICommand command, ICommandDispatchContext context)
        {
            if (command is IUserContextCommand userContextCommand)
            {
                properties["UserId"] = userContextCommand.AuthenticatedUserId.ToString();
            }
        }
    }
}
