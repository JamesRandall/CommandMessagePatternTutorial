using System;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.Services
{
    public interface ICheckoutService
    {
        Task<Order> CreateOrderAsync(Guid userId);
        Task<bool> ConfirmPaymentAsync(Guid orderId);
    }
}