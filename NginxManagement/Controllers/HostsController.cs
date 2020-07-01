using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NginxManagement.Managers;
using System.Threading.Tasks;

namespace NginxManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HostsController : ControllerBase
    {
        private readonly ILogger<HostsController> _logger;
        private readonly IHostsManager hostsManager;

        public HostsController(ILogger<HostsController> logger, IHostsManager hostsManager)
        {
            _logger = logger;
            this.hostsManager = hostsManager;
        }

        [HttpPut]
        public async Task<IActionResult> Create(string name, string ipAddress, int? userId) 
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ipAddress) || userId == null)
                return BadRequest("Not all parameters were provided");
            
            await hostsManager.Create(name, ipAddress, userId.Value);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(string name, int? userId)
        {
            if (string.IsNullOrEmpty(name) || userId == null)
                return BadRequest("Not all parameters were provided");

            var status  = await hostsManager.Remove(name, userId.Value);

            return StatusCode((int)status);
        }
    }
}
