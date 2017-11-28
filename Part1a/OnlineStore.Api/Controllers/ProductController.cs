using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Model;
using OnlineStore.Services;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<StoreProduct> Get(Guid id)
        {
            return await _productService.GetAsync(id);
        }
    }
}
