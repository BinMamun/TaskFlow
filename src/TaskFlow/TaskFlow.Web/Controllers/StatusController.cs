using System.Web;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Domain.ServiceInterfaces;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Controllers
{
    public class StatusController(
        ILogger<StatusController> logger, 
        IStatusService statusService) : Controller
    {
        private readonly ILogger<StatusController> _logger = logger;
        private readonly IStatusService _statusService = statusService;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetStatusJsonData([FromBody] StatusListModel model)
        {
            var result = await _statusService.GetAllStatusAsync(
                model.PageIndex,
                model.PageSize,
                model.Search,
                model.FormatSortExpression("StatusName", "StatusDescription"));

            var data = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from records in result.data
                        select new string[]
                            {
                                HttpUtility.HtmlEncode(records.StatusName),
                                HttpUtility.HtmlEncode(records.StatusDescription),
                                records.Id.ToString()
                            }
                        ).ToArray()
            };
            return Json(data);
        }
    }
}
