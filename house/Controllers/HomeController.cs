using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace house.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogDebug("Index");

            return View();
        }

        public IActionResult Error() => View();
    }
}
