using TaskFlow.Domain.Entities;

namespace TaskFlow.Web.Models
{
    public class TaskEditModel : TaskCreateModel
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
    }
}
