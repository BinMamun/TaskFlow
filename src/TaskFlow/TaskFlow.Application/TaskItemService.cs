using TaskFlow.Domain;
using TaskFlow.Domain.ServiceInterfaces;

namespace TaskFlow.Application
{
    public class TaskItemService(ITaskFlowUnitOfWork taskUnitOfWork) : ITaskItemService
    {
        private readonly ITaskFlowUnitOfWork _taskUnitOfWork = taskUnitOfWork;
    }
}
