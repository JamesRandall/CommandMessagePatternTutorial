using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineStore.Model;
using OnlineStore.DataAccess;

namespace OnlineStore.Services.Implementation
{
    internal class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartRepository _repository;
        private readonly ILogger _logger;

        public ShoppingCartService(
            IProductService productService,
            IShoppingCartRepository repository,
            ILogger logger)
        {
            _productService = productService;
            _repository = repository;
            _logger = logger;
        }

        public async Task AddToBasketAsync(Guid userId, Guid productId, int quantity)
        {
            _logger.LogInformation("Updating basket for user {0}", userId);
            try
            {
                Model.ShoppingCart cart = await GetBasket(userId);
                StoreProduct product = await _productService.GetAsync(productId);
                if (product == null)
                {
                    _logger.LogWarning("Product {0} can not be added to cart for user {1} as it does not exist", productId, userId);
                    throw new ShoppingCartException($"Product {productId} does not exist");
                }
                List<ShoppingCartItem> cartItems = new List<ShoppingCartItem>(cart.Items);
                cartItems.Add(new ShoppingCartItem
                {
                    Product = product,
                    Quantity = quantity
                });
                cart.Items = cartItems;
                await _repository.UpdateAsync(cart);
                _logger.LogInformation("Updated basket for user {0}", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to add product {0} to basket for user {1}", productId, userId);
                throw new ShoppingCartException("Unable to add to basket");
            }
        }

        public async Task<Model.ShoppingCart> GetAsync(Guid userId)
        {
            _logger.LogInformation("Getting basket for user {0}", userId);
            try
            {
                Model.ShoppingCart cart = await GetBasket(userId);
                _logger.LogInformation("Retrieved cart for user {0} with {1} items", userId, cart.Items.Count);
                return cart;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unable to get basket for user {0}", userId);
                return null;
            }
        }

        public Task ClearAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        private async Task<Model.ShoppingCart> GetBasket(Guid userId)
        {
            Model.ShoppingCart cart = await _repository.GetAsync(userId);
            if (cart == null)
            {
                cart = new Model.ShoppingCart
                {
                    UserId = userId
                };
            }
            return cart;
        }
    }
}

