using System.Net;
using System.Threading.Tasks;

namespace NginxManagement.Managers
{
    public interface IHostsManager
    {
        Task Create(string name, string ipAddress, int userId);
        Task<HttpStatusCode> Remove(string name, int userId);
    }
}