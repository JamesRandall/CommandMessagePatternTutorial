using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineStore.DataAccess;
using OnlineStore.Model;

namespace OnlineStore.Services.Implementation
{
    internal class CheckoutService : ICheckoutService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger _logger;

        public CheckoutService(IOrderRepository orderRepository,
            IShoppingCartService shoppingCartService,
            ILogger logger)
        {
            _orderRepository = orderRepository;
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        public async Task<Order> CreateOrderAsync(Guid userId)
        {
            _logger.LogInformation("Creating order for user {0} from basket", userId);
            try
            {
                ShoppingCart cart = await _shoppingCartService.GetAsync(userId);
                if (cart.Items.Count == 0)
                {
                    throw new ShoppingCartException("Shopping cart must not be empty to checkout");
                }
                Order order = new Order
                {
                    CreatedAtUtc = DateTime.UtcNow,
                    OrderItems = cart.Items.Select(x => new OrderItem
                    {
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        ProductId = x.Product.Id,
                        Quantity = x.Quantity
                    }).ToArray(),
                    PaymentMade = false,
                    PercentageDiscountApplied = 10,
                    UserId = userId
                };
                order.Total = order.OrderItems.Sum(i => i.Price * i.Quantity) * (100-order.PercentageDiscountApplied) / 100.0;
                await _orderRepository.CreateAsync(order);
                _logger.LogInformation("Created order for user {0} from basket", userId);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to create order for user {0}", userId);
                throw new CheckoutException($"Unable to create order for user {userId}");
            }
            
        }

        public async Task<bool> ConfirmPaymentAsync(Guid orderId)
        {
            _logger.LogInformation("Confirming payment for order {0} from basket", orderId);
            try
            {
                Order order = await _orderRepository.GetAsync(orderId);
                order.PaymentMade = true;
                await _orderRepository.UpdateAsync(order);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to confirm payment for order {0}", orderId);
                throw new CheckoutException($"Unable to confirm payment for order {orderId}");
            }
        }
    }
}
