using TaskFlow.Domain;
using TaskFlow.Web.Data;

namespace TaskFlow.Infrastructure.UnitOfWork
{
    public class TaskFlowUnitOfWork : UnitOfWork, ITaskFlowUnitOfWork
    {
        public TaskFlowUnitOfWork(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
