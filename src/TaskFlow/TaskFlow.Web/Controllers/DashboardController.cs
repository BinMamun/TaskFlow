using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Domain.ServiceInterfaces;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Controllers
{
    public class DashboardController(
        ILogger<DashboardController> logger,
        ITaskItemService taskItemService
        ) : Controller
    {
        private readonly ILogger<DashboardController> _logger = logger;
        private readonly ITaskItemService _taskItemService = taskItemService;

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel();
            model.Pending = await _taskItemService.PendingTaskCountAsync();
            model.InProgress = await _taskItemService.InProgressTaskCountAsync();
            model.Completed = await _taskItemService.CompletedTaskCountAsync();
            return View(model);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
