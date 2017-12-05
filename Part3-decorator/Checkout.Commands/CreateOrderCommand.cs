using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Model;
using Core.Model;

namespace Checkout.Commands
{
    public class CreateOrderCommand : ICommand<CommandResponse<Order>>, IUserContextCommand, ILogAwareCommand
    {
        public Guid AuthenticatedUserId { get; set; }

        public string GetPreDispatchLogMessage()
        {
            return $"Creating order for user {AuthenticatedUserId} from basket";
        }

        public string GetPostDispatchLogMessage()
        {
            return $"Created order for user {AuthenticatedUserId} from basket";
        }

        public string GetDispatchErrorLogMessage()
        {
            return $"Unable to create order for user {AuthenticatedUserId}";
        }
    }
}
