using SimpleWebApp.Domain.Abstracts;

namespace SimpleWebApp.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }
        public List<KeyValuePair<KeyValuePair<Guid, string>, uint>> RegistredProducts { get; set; }
    }
}
