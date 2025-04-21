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

        //[HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePolicy")]
        //public async Task<IActionResult> Create(StatusCreateModel model)
        //{
        //    var category = _mapper.Map<ItemCategory>(model);
        //    category.Id = Guid.NewGuid();
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _categoryManagementService.CreateItemCategoryAsync(category);

        //            TempData.Put("ResponseMessage", new ResponseModel()
        //            {
        //                Message = "Category created Successfully",
        //                Type = ResponseTypes.Success
        //            });
        //            return RedirectToAction("Index");
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData.Put("ResponseMessage", new ResponseModel()
        //            {
        //                Message = "Catgory creation failed",
        //                Type = ResponseTypes.Danger
        //            });
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
