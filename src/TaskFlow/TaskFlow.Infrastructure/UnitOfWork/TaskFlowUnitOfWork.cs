using TaskFlow.Domain;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.UnitOfWork
{
    public class TaskFlowUnitOfWork : UnitOfWork, ITaskFlowUnitOfWork
    {
        public TaskFlowUnitOfWork(
            ApplicationDbContext dbContext,
            IStatusRepository statusRepository,
            ITaskItemRepository taskItemRepository,
            ITaskDependencyRepository taskDependencyRepository
            ) : base(dbContext)
        {
            StatusRepository = statusRepository;
            TaskItemRepository = taskItemRepository;
            TaskDependencyRepository = taskDependencyRepository;
        }

        public IStatusRepository StatusRepository { get; private set; }
        public ITaskItemRepository TaskItemRepository { get; private set; }
        public ITaskDependencyRepository TaskDependencyRepository { get; private set; }

    }
}
