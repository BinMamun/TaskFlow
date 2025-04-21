using Microsoft.EntityFrameworkCore;
using TaskFlow.Application;
using TaskFlow.Domain;
using TaskFlow.Domain.RepositoryContracts;
using TaskFlow.Domain.ServiceInterfaces;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.UnitOfWork;
using TaskFlow.Web.Data;

namespace TaskFlow.Web
{
    public static class ServiceCollectionExtensions
    {
        public static void ServiceRegistration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseNpgsql(connectionString));

            services.AddScoped<ITaskFlowUnitOfWork, TaskFlowUnitOfWork>();
            
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddScoped<ITaskItemService, TaskItemService>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();

        }
    }
}
