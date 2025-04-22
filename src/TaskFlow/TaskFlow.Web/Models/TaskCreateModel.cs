using Microsoft.AspNetCore.Mvc.Rendering;
using TaskFlow.Domain.Entities;
using TaskFlow.Infrastructure;

namespace TaskFlow.Web.Models
{
    public class TaskCreateModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public Guid? StatusId { get; set; }
        public Priority Priority { get; set; }
        public List<Guid>? PrerequisiteIds { get; set; }
        public IList<SelectListItem>? AllTasks { get; private set; }

        public void GetAllTasks(IList<TaskItem> taskItems)
        {
            AllTasks = taskItems.ToSelectList(x => x.Title, y => y.Id.ToString());
        }
    }
}
