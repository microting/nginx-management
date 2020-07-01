using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NginxManagement.Infrastructure.Data;
using System.Threading.Tasks;

namespace NginxManagement.Filters
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly NginxManagemenDbContext dbContext;

        public AuthorizationFilter(NginxManagemenDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Query["token"].ToString();

            var dbToken = await dbContext.PluginConfigurationValues.FirstOrDefaultAsync(t => t.Name == "BaseSettings:ServiceAccessToken");
            if (string.IsNullOrEmpty(token) || token != dbToken.Value)
                context.Result = new UnauthorizedResult();
        }
    }
}
