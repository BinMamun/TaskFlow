using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.RepositoryContracts
{
    public interface IStatusRepository : IRepositoryBase<Status, Guid>
    {
        Task<(IList<Status> data, int total, int totalDisplay)> GetDynamicStatusAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
