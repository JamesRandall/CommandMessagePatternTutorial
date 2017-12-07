using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Application.Repositories;
using Checkout.Commands;
using Checkout.Model;
using Core.Model;
using Microsoft.Extensions.Logging;

namespace Checkout.Application.Handlers
{
    internal class MakePaymentCommandHandler : ICommandHandler<MakePaymentCommand, CommandResponse>
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<MakePaymentCommand> _logger;

        public MakePaymentCommandHandler(
            IOrderRepository repository,
            ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<MakePaymentCommand>();
        }

        public async Task<CommandResponse> ExecuteAsync(MakePaymentCommand command, CommandResponse previousResult)
        {
            _logger.LogInformation("Confirming payment for order {0} from basket", command.OrderId);
            try
            {
                Order order = await _repository.GetAsync(command.OrderId);
                order.PaymentMade = true;
                await _repository.UpdateAsync(order);
                return CommandResponse.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to confirm payment for order {0}", command.OrderId);
                return CommandResponse.WithError($"Unable to confirm payment for order {command.OrderId}");
            }
        }
    }
}
