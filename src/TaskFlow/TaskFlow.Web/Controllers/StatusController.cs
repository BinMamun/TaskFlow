using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Domain.Entities;
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


        public IActionResult Create()
        {
            var model = new StatusCreateModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatusCreateModel model)
        {

            if (ModelState.IsValid)
            {
                var status = new Status()
                {
                    Id = Guid.NewGuid(),
                    StatusName = model.StatusName,
                    StatusDescription = model.StatusDescription
                };

                await _statusService.CreateStatusAsync(status);

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var status = await _statusService.GetStatusById(id);
            var model = new StatusEditModel()
            {
                Id = id,
                StatusName = status.StatusName,
                StatusDescription = status.StatusDescription
            };

            return View(model);
        }


       
    }
}
