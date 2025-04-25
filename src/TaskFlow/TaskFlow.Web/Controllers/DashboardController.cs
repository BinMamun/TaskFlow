using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Web.Controllers
{
    public class DashboardController(ILogger<DashboardController> logger) : Controller
    {
        private readonly ILogger<DashboardController> _logger = logger;

        public IActionResult Index()
        {
            return View();
        }
    }
}
