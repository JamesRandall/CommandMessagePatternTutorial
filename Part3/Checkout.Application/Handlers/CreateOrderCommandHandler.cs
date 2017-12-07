using System;
using System.Linq;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Application.Repositories;
using Checkout.Commands;
using Checkout.Model;
using Core.Model;
using ShoppingCart.Commands;
using ApplicationException = Core.Model.ApplicationException;

namespace Checkout.Application.Handlers
{
    class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CommandResponse<Order>>
    {
        private readonly IOrderRepository _repository;
        private readonly ICommandDispatcher _dispatcher;

        public CreateOrderCommandHandler(
            IOrderRepository repository,
            ICommandDispatcher dispatcher)
        {
            _repository = repository;
            _dispatcher = dispatcher;
        }
        
        public async Task<CommandResponse<Order>> ExecuteAsync(CreateOrderCommand command, CommandResponse<Order> previousResult)
        {
            try
            {
                ShoppingCart.Model.ShoppingCart cart = (await _dispatcher.DispatchAsync(new GetCartQuery{ AuthenticatedUserId =  command.AuthenticatedUserId})).Result;
                if (cart.Items.Count == 0)
                {
                    return CommandResponse<Order>.WithError("Shopping cart must not be empty to checkout");
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
                return CommandResponse<Order>.Ok(order);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unable to create order for user {command.AuthenticatedUserId}", ex);
            }
        }
    }
}
