namespace NginxManagement.Infrastructure.Data.Entities
{
    public class Host : BaseEntity
    {
        public string Name { get; set; }
        public string Ip { get; set; }
    }
}
