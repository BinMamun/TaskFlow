using TaskFlow.Domain.RepositoryContracts;

namespace TaskFlow.Domain
{
    public interface ITaskFlowUnitOfWork : IUnitOfWork
    {
        public IStatusRepository StatusRepository { get; }
        public ITaskItemRepository TaskItemRepository { get; }
        public ITaskDependencyRepository TaskDependencyRepository { get; }
    }
}
