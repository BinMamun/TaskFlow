using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.RepositoryContracts
{
    public interface ITaskItemRepository : IRepositoryBase<TaskItem, Guid>
    {
    }
}
