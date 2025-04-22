namespace TaskFlow.Domain.Entities
{
    public class TaskDependency
    {
        public Guid TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }

        public Guid PrerequisiteTaskId { get; set; }
        public TaskItem? PrerequisiteTask { get; set; }
    }

}
