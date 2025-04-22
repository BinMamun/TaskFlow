using TaskFlow.Domain;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.ServiceInterfaces;

namespace TaskFlow.Application
{
    public class TaskItemService(ITaskFlowUnitOfWork taskUnitOfWork) : ITaskItemService
    {
        private readonly ITaskFlowUnitOfWork _taskUnitOfWork = taskUnitOfWork;

        public async Task<(IList<TaskItem> data, int total, int totalDisplay)> GetAllTasksAsync(int pageIndex, int pageSize, TaskItemDto search, string? order)
        {
            return await _taskUnitOfWork.TaskItemRepository.GetAllTaskItemsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<TaskItem>> GetTaskListAsync()
        {
            return await _taskUnitOfWork.TaskItemRepository.GetAllAsync();
        }

        public async Task<IList<Status>> GetStatusListAsync()
        {
            return await _taskUnitOfWork.StatusRepository.GetAllAsync();
        }
    }
}
