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
            Order order = await _repository.GetAsync(command.OrderId);
            order.PaymentMade = true;
            await _repository.UpdateAsync(order);
            return CommandResponse.Ok();
        }
    }
}
