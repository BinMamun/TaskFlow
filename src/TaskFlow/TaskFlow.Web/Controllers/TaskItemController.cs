using System.Web;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application;
using TaskFlow.Domain.ServiceInterfaces;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Controllers
{
    public class TaskItemController(
        ILogger<TaskItemController> logger, 
        ITaskItemService taskItemService) : Controller
    {
        private readonly ILogger<TaskItemController> _logger = logger;
        private readonly ITaskItemService _taskItemService = taskItemService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetTaskItemJsonData([FromBody] TaskListModel model)
        {
            var result = await _taskItemService.GetAllTasksAsync(
                model.PageIndex,
                model.PageSize,
                model.Search,
                model.FormatSortExpression("Title", "Description", "DueDate", "StatusId", "Status", "Priority"));

            var data = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from records in result.data
                        select new string[]
                            {
                                HttpUtility.HtmlEncode(records.DueDate.ToString("dd-MMM-yyyy")),
                                HttpUtility.HtmlEncode(records.Title),
                                HttpUtility.HtmlEncode(records.Priority),
                                HttpUtility.HtmlEncode(records.Status.StatusName),
                                records.Id.ToString()
                            }
                        ).ToArray()
            };
            return Json(data);
        }
    }
}
