using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NginxManagement.Infrastructure.Data;
using NginxManagement.Infrastructure.Data.Entities;
using NginxManagement.Infrastructure.FileSystem;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NginxManagement.Managers
{
    public class HostsManager : IHostsManager
    {
        private readonly ILogger<HostsManager> logger;
        private readonly NginxManagemenDbContext dbContext;
        private readonly ITemplateStorage templateStorage;

        public HostsManager(ILogger<HostsManager> logger, NginxManagemenDbContext dbContext, ITemplateStorage templateStorage)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.templateStorage = templateStorage;
        }

        public async Task Create(string name, string ipAddress, int userId)
        {
            var template = templateStorage.GetTemplate("Template.txt");

            var entity = new Host() { Name = name, Ip = ipAddress, CreatedByUserId = userId };
            await entity.Create(dbContext);

            var config = await dbContext.PluginConfigurationValues.ToDictionaryAsync(t => t.Name);
            var serverName = $"{name}.{config["BaseSettings:Host"].Value}";
            var accessLog = string.Format(config["BaseSettings:AccessLogTemplate"].Value, name);
            var errorLog = string.Format(config["BaseSettings:ErrorLogTemplate"].Value, name);

            var configFileTemplate = string.Format(template, serverName, accessLog, errorLog, ipAddress);

            templateStorage.Save(config["BaseSettings:DestinationPath"].Value, serverName, configFileTemplate);

            var command = config["BaseSettings:PostExecuteCommand"].Value.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            var result = RunCommand(command[0], string.Join(' ', command.Skip(1)));
            logger.LogInformation("Command execution result: {result}", result);
        }

        private string RunCommand(string command, string args)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments  = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (string.IsNullOrEmpty(error))
                return output;

            return error;
        }

        public async Task<HttpStatusCode> Remove(string name, int userId)
        {
            var config = await dbContext.PluginConfigurationValues.ToDictionaryAsync(t => t.Name);
            var serverName = $"{name}.{config["BaseSettings:Host"].Value}";

            var host = await dbContext.Hosts.OrderBy(t => t.Id).LastOrDefaultAsync(t => t.Name == name);
            if (host == null)
                return HttpStatusCode.NotFound;

            var updatedHost = new Host() {
                Id = host.Id,
                Name = host.Name,
                Ip = host.Ip,
                UpdatedByUserId = userId
            };
            await updatedHost.Delete<Host>(dbContext);

            templateStorage.Remove(config["BaseSettings:DestinationPath"].Value, serverName);

            return HttpStatusCode.OK;
        }
    }
}
