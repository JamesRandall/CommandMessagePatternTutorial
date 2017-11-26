using System;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.DataAccess
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task<Order> GetAsync(Guid orderId);
    }
}