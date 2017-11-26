using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Extensions;
using OnlineStore.Model;
using OnlineStore.Services;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _service;

        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ShoppingCart> Get()
        {
            return await _service.GetAsync(this.GetUserId());
        }

        [HttpPut("{productId}/{quantity}")]
        public async Task<IActionResult> Put(Guid productId, int quantity)
        {
            try
            {
                await _service.AddToBasketAsync(this.GetUserId(), productId, quantity);
                return Ok();
            }
            catch (ShoppingCartException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _service.ClearAsync(this.GetUserId());
        }
    }
}
