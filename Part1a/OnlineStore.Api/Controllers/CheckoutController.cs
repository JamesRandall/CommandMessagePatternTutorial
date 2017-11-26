using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Extensions;
using OnlineStore.Model;
using OnlineStore.Services;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("createOrder")]
        public async Task<Order> Post()
        {
            return await _checkoutService.CreateOrderAsync(this.GetUserId());
        }

        [HttpPost("pay/{orderId}")]
        public async Task<Order> Put(Guid orderId)
        {
            return await _checkoutService.CreateOrderAsync(this.GetUserId());
        }
    }
}
