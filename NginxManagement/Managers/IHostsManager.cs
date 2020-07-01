using System.Threading.Tasks;

namespace NginxManagement.Managers
{
    public interface IHostsManager
    {
        Task Create(string name, string ipAddress, int userId);
        Task Remove(string name, int userId);
    }
}