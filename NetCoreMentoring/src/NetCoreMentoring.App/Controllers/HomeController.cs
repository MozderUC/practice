using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            _productService.GetProducts();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
