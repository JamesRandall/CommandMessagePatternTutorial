using System.Collections.Generic;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Repositories;
using ShoppingCart.Commands;
using ShoppingCart.Model;
using Store.Commands;
using Store.Model;

namespace ShoppingCart.Application.Handlers
{
    internal class AddToCartCommandHandler : ICommandHandler<AddToCartCommand, CommandResponse>
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly IShoppingCartRepository _repository;
        private readonly ILogger<AddToCartCommandHandler> _logger;

        public AddToCartCommandHandler(ILoggerFactory loggerFactory,
            ICommandDispatcher dispatcher,
            IShoppingCartRepository repository)
        {
            _dispatcher = dispatcher;
            _repository = repository;
            _logger = loggerFactory.CreateLogger<AddToCartCommandHandler>();
        }

        public async Task<CommandResponse> ExecuteAsync(AddToCartCommand command, CommandResponse previousResult)
        {
            Model.ShoppingCart cart = await _repository.GetActualOrDefaultAsync(command.AuthenticatedUserId);

            StoreProduct product = (await _dispatcher.DispatchAsync(new GetStoreProductQuery{ProductId = command.ProductId})).Result;

            if (product == null)
            {
                _logger.LogWarning("Product {0} can not be added to cart for user {1} as it does not exist", command.ProductId, command.AuthenticatedUserId);
                return CommandResponse.WithError($"Product {command.ProductId} does not exist");
            }
            List<ShoppingCartItem> cartItems = new List<ShoppingCartItem>(cart.Items);
            cartItems.Add(new ShoppingCartItem
            {
                Product = product,
                Quantity = command.Quantity
            });
            cart.Items = cartItems;
            await _repository.UpdateAsync(cart);
            _logger.LogInformation("Updated basket for user {0}", command.AuthenticatedUserId);
            return CommandResponse.Ok();
        }
    }
}
