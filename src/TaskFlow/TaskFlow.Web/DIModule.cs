using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain;
using TaskFlow.Infrastructure.UnitOfWork;
using TaskFlow.Web.Data;

namespace TaskFlow.Web
{
    public static class DIModule
    {
        public static void ServiceRegistration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseNpgsql(connectionString));

            services.AddScoped<ITaskFlowUnitOfWork, TaskFlowUnitOfWork>();
        }
    }
}
