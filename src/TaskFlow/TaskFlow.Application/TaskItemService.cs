using System.Threading.Tasks;
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

        public async Task CreateNewTaskAsync(TaskItem taskItem)
        {
            await _taskUnitOfWork.TaskItemRepository.AddAsync(taskItem);
            await _taskUnitOfWork.SaveAsync();
        }

        public async Task CreateNewDependencyAsync(Guid taskId, List<Guid> prerequisiteIds)
        {
            if (taskId != Guid.Empty && prerequisiteIds != null)
            {
                foreach (var prerequisiteId in prerequisiteIds)
                {
                    var taskDepenedency = new TaskDependency()
                    {
                        Id = Guid.NewGuid(),
                        TaskItemId = taskId,
                        PrerequisiteTaskId = prerequisiteId
                    };
                    await _taskUnitOfWork.TaskDependencyRepository.AddAsync(taskDepenedency);
                }
            }
            await _taskUnitOfWork.SaveAsync();
        }

        public Task<TaskItem> GetTaskAsync(Guid id)
        {
            return _taskUnitOfWork.TaskItemRepository.GetTaskWithPrerequisites(id);

        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            await _taskUnitOfWork.TaskItemRepository.EditAsync(task);
            await _taskUnitOfWork.SaveAsync();
        }

        public async Task UpdateDependencyAsync(Guid taskId, List<Guid>? prerequisiteIds)
        {
            if (prerequisiteIds == null || prerequisiteIds.Count() <= 0)
            {
                return;
            }

            var existingDependencies = await _taskUnitOfWork.TaskDependencyRepository
                .GetDependenciesAsync(taskId);

            foreach (var dep in existingDependencies)
            {
                await _taskUnitOfWork.TaskDependencyRepository.RemoveAsync(dep);
            }

            foreach (var prerequisiteId in prerequisiteIds)
            {
                var taskDependency = new TaskDependency
                {
                    Id = Guid.NewGuid(),
                    TaskItemId = taskId,
                    PrerequisiteTaskId = prerequisiteId
                };

                await _taskUnitOfWork.TaskDependencyRepository.AddAsync(taskDependency);
            }

            await _taskUnitOfWork.SaveAsync();
        }

    }
}
