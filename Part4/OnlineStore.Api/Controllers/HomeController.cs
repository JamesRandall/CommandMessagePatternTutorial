using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Api.Controllers
{
    public class HomeController : Controller
    {
        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
