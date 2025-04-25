using Microsoft.EntityFrameworkCore;
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

        public async Task<(IList<TaskItem> data, int total, int totalDisplay)> GetAllTaskItemsAsync(int pageIndex, int pageSize, TaskItemDto search, string? order)
        {
            var query = _dbSet.AsQueryable();

            var total = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(search.StatusId) && Guid.TryParse(search.StatusId, out var statusId))
            {
                query = query.Where(p => p.StatusId.Equals(statusId));
            }

            if (search.Priority != null)
            {
                query = query.Where(p => (int)p.Priority == search.Priority);
            }

            if (search.FromDate.HasValue)
            {
                query = query.Where(p => p.DueDate >= DateTime.SpecifyKind((DateTime)search.FromDate, DateTimeKind.Utc));
            }

            if (search.ToDate.HasValue)
            {
                query = query.Where(p => p.DueDate <= DateTime.SpecifyKind((DateTime)search.ToDate, DateTimeKind.Utc));
            }

            var (data, totalDisplay) = await GetDynamicDataAsync(query, order, x => x.Include(y => y.Status), pageIndex, pageSize, true);

            return (data, total, totalDisplay);
        }

        public async Task<TaskItem?> GetTaskWithPrerequisites(Guid id)
        {

            return await _dbSet
                            .Include(x => x.PrerequisiteLinks)
                            .Include(x => x.Status)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetPendingTaskCountAsync()
        {
            return await GetCountAsync(x => x.Status.StatusName == "Pending");
        }

        public async Task<int> GetInProgressTaskCountAsync()
        {
            return await GetCountAsync(x => x.Status.StatusName == "In-Progress");
        }

        public async Task<int> GetCompletedTaskCountAsync()
        {
            return await GetCountAsync(x => x.Status.StatusName == "Completed");
        }

        public async Task<IList<TaskItem>> GetUpcomingDeadLineTasks()
        {
            var plusSevenDays = DateTime.UtcNow.AddDays(7);
            var upcomingTasks = await GetAsync(
                                filter: x => x.DueDate <= plusSevenDays && x.Status.StatusName != "Completed" ,
                                orderBy: x => x.OrderBy(y => y.DueDate),
                                include: x => x.Include(y => y.Status),
                                isTrackingOff: false);

            return upcomingTasks;
        }
    }
}
