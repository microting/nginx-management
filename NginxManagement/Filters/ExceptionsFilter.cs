using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace NginxManagement.Filters
{
    public class ExceptionsFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionsFilter> logger;

        public ExceptionsFilter(ILogger<ExceptionsFilter> logger)
        {
            this.logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            logger.LogError(context.Exception, "There was an error during request execution");
            context.ExceptionHandled = true;
            context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);

            return Task.CompletedTask;
        }
    }
}
