using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Extensions;
using ShoppingCart.Model;
using ShoppingCart.Services;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingBasketController : Controller
    {
        private readonly IShoppingBasketService _service;

        public ShoppingBasketController(IShoppingBasketService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<ShoppingBasket> Get()
        {
            return await _service.Get(this.GetUserId());
        }

        // PUT api/values/5
        [HttpPut("{productId}/{quantity}")]
        public async Task Put(Guid productId, int quantity)
        {
            await _service.AddToBasket(this.GetUserId(), productId, quantity);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Clear(this.GetUserId());
        }
    }
}
