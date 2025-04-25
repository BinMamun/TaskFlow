using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Domain.ServiceInterfaces;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Controllers
{
    [Authorize]
    public class DashboardController(
        ILogger<DashboardController> logger,
        ITaskItemService taskItemService
        ) : Controller
    {
        private readonly ILogger<DashboardController> _logger = logger;
        private readonly ITaskItemService _taskItemService = taskItemService;

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                Pending = await _taskItemService.PendingTaskCountAsync(),
                InProgress = await _taskItemService.InProgressTaskCountAsync(),
                Completed = await _taskItemService.CompletedTaskCountAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetUpcomingDeadLineTasksJsonData([FromBody] TaskListModel model)
        {
            var result = await _taskItemService.GetAllTasksAsync(
                model.PageIndex,
                model.PageSize,
                model.SearchParams,
                model.FormatSortExpression("DueDate", "Title", "Description", "Priority", "StatusId", "StatusName"));

            var data = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from records in result.data
                        select new string[]
                            {
                                HttpUtility.HtmlEncode(records.DueDate.ToString("dd-MMM-yyyy")),
                                HttpUtility.HtmlEncode(records.Title),
                                HttpUtility.HtmlEncode(records.Description),
                                HttpUtility.HtmlEncode(records.Priority),
                                HttpUtility.HtmlEncode(records.Status.StatusName.ToString()),
                                records.Id.ToString()
                            }
                        ).ToArray()
            };
            return Json(data);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
