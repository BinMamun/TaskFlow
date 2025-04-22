using TaskFlow.Domain;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskItemRepository : Repository<TaskItem, Guid>, ITaskItemRepository
    {
        public TaskItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IList<TaskItem> data, int total, int totalDisplay)> GetAllTaskItemsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            var searchText = search.Value;

            if (string.IsNullOrWhiteSpace(searchText))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);

            else
                return await GetDynamicAsync(x => 
                                        x.Title.Contains(searchText) ||
                                        x.Description.Contains(searchText),
                                        order, null, pageIndex, pageSize, true);
        }
    }
}
