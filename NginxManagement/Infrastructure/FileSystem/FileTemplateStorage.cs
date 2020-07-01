using System.IO;

namespace NginxManagement.Infrastructure.FileSystem
{
    public class FileTemplateStorage : ITemplateStorage
    {
        public string GetTemplate(string path) => File.ReadAllText(path);

        public void Save(string path, string name, string content)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(Path.Combine(path, name), content);
        }

        public void Remove(string path, string name)
        {
            if (!Directory.Exists(path))
                return;

            File.Delete(Path.Combine(path, name));
        }
    }
}
