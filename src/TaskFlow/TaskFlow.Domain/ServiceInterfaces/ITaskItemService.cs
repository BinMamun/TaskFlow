
using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.ServiceInterfaces
{
    public interface ITaskItemService
    {
        Task<(IList<TaskItem> data, int total, int totalDisplay)> GetAllTasksAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
