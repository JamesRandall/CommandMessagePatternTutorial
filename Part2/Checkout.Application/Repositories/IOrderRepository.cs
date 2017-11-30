using System;
using System.Threading.Tasks;
using Checkout.Model;

namespace Checkout.Application.Repositories
{
    internal interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task<Order> GetAsync(Guid orderId);
    }
}