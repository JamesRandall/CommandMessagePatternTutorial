using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Application.Repositories;
using Checkout.Commands;
using Checkout.Model;
using Core.Model;
using Microsoft.Extensions.Logging;
using ShoppingCart.Commands;

namespace Checkout.Application.Handlers
{
    class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CommandResponse<Order>>
    {
        private readonly IOrderRepository _repository;
        private readonly ICommandDispatcher _dispatcher;
        private readonly ILogger<CreateOrderCommand> _logger;

        public CreateOrderCommandHandler(
            IOrderRepository repository,
            ILoggerFactory loggerFactory,
            ICommandDispatcher dispatcher)
        {
            _repository = repository;
            _dispatcher = dispatcher;
            _logger = loggerFactory.CreateLogger<CreateOrderCommand>();
        }
        
        public async Task<CommandResponse<Order>> ExecuteAsync(CreateOrderCommand command, CommandResponse<Order> previousResult)
        {
            _logger.LogInformation("Creating order for user {0} from basket", command.AuthenticatedUserId);
            try
            {

                ShoppingCart.Model.ShoppingCart cart = (await _dispatcher.DispatchAsync(new GetCartQuery{ AuthenticatedUserId =  command.AuthenticatedUserId})).Result;
                if (cart.Items.Count == 0)
                {
                    return new CommandResponse<Order> {  ErrorMessage = "Shopping cart must not be empty to checkout", IsSuccess = false};
                }
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    CreatedAtUtc = DateTime.UtcNow,
                    OrderItems = cart.Items.Select(x => new OrderItem
                    {
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        ProductId = x.Product.Id,
                        Quantity = x.Quantity
                    }).ToArray(),
                    PaymentMade = false,
                    PercentageDiscountApplied = 10.0,
                    UserId = command.AuthenticatedUserId
                };
                order.Total = order.OrderItems.Sum(i => i.Price * i.Quantity) * (100-order.PercentageDiscountApplied) / 100.0;
                await _repository.CreateAsync(order);
                _logger.LogInformation("Created order for user {0} from basket", command.AuthenticatedUserId);
                return CommandResponse<Order>.Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to create order for user {0}", command.AuthenticatedUserId);
                throw new CheckoutException($"Unable to create order for user {command.AuthenticatedUserId}");
            }
        }
    }
}
