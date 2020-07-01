using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NginxManagement.Filters;
using NginxManagement.Infrastructure.Data;
using NginxManagement.Infrastructure.FileSystem;
using NginxManagement.Managers;

namespace NginxManagement
{
    public static class NginxManagementStartupExtensions
    {

        public static IServiceCollection AddNginxManagementServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<AuthorizationFilter>();
            services.AddScoped<ExceptionsFilter>();
            services.AddScoped<IHostsManager, HostsManager>();
            services.AddScoped<ITemplateStorage, FileTemplateStorage>();

            services.AddDbContext<NginxManagemenDbContext>(options =>
                       options.UseMySQL(connectionString));
            return services;
        }
    }
}
