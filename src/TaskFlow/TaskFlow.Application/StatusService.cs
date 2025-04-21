using TaskFlow.Domain;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.ServiceInterfaces;

namespace TaskFlow.Application
{
    public class StatusService(ITaskFlowUnitOfWork taskFlowUnitOfWork) : IStatusService
    {
        private readonly ITaskFlowUnitOfWork _taskFlowUnitOfWork = taskFlowUnitOfWork;


        public async Task<(IList<Status> data, int total, int totalDisplay)> GetAllStatusAsync()
        {
            return await _taskFlowUnitOfWork.StatusRepository.GetDynamicStatusAsync();
        }
    }
}
