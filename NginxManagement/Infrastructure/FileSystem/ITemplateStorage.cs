namespace NginxManagement.Infrastructure.FileSystem
{
    public interface ITemplateStorage
    {
        string GetTemplate(string path);
        void Remove(string path, string name);
        void Save(string path, string name, string content);
    }
}