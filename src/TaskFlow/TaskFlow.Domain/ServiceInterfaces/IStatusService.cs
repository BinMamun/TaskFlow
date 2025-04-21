using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.ServiceInterfaces
{
    public interface IStatusService
    {
        Task<(IList<Status> data, int total, int totalDisplay)> GetAllStatusAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);

        Task CreateStatusAsync(Status status);
        Task<Status> GetStatusById(Guid id);
    }
}
