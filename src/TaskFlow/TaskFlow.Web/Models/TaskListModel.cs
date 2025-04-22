using TaskFlow.Domain;

namespace TaskFlow.Web.Models
{
    public class TaskListModel : Datatables
    {
        public TaskItemDto Search { get; set; }
    }
}
