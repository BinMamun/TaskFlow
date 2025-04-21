using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class StatusRepository : Repository<Status, Guid>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<(IList<Status> data, int total, int totalDisplay)> GetDynamicStatusAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            var searchText = search.Value;
            if (string.IsNullOrWhiteSpace(searchText))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);

            else
                return await GetDynamicAsync(x => x.StatusName.Contains(searchText)
                                             , order, null, pageIndex, pageSize, true);
        }
    }
}
