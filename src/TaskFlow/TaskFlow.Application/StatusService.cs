using TaskFlow.Domain;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.ServiceInterfaces;

namespace TaskFlow.Application
{
    public class StatusService(ITaskFlowUnitOfWork taskFlowUnitOfWork) : IStatusService
    {
        private readonly ITaskFlowUnitOfWork _taskFlowUnitOfWork = taskFlowUnitOfWork;


        public async Task<(IList<Status> data, int total, int totalDisplay)> GetAllStatusAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _taskFlowUnitOfWork.StatusRepository.GetDynamicStatusAsync(pageIndex, pageSize, search, order);
        }

        public async Task CreateStatusAsync(Status status)
        {
            await _taskFlowUnitOfWork.StatusRepository.AddAsync(status);
            await _taskFlowUnitOfWork.SaveAsync();
        }

        public async Task<Status> GetStatusById(Guid id)
        {
            return await _taskFlowUnitOfWork.StatusRepository.GetByIdAsync(id);
        }

        public async Task UpdateStatusAsync(Status status)
        {
            await _taskFlowUnitOfWork.StatusRepository.EditAsync(status);
            await _taskFlowUnitOfWork.SaveAsync();
        }
    }
}
