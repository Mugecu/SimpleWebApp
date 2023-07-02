using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Domain.DTOs
{
    public class WarehouseDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public List<KeyValuePair<KeyValuePair<Guid, string>, uint>> RegistredProducts { get; set; }

        public WarehouseDTO ToDto(Warehouse model)
        {
            Id= model.Id;
            Name= model.Name;
            RegistredProducts = model.RegistredProducts;

            return this;
        }
        public Warehouse ToModel()
            => Id.HasValue 
                ? new Warehouse(Id.Value, Name, RegistredProducts)
                : new Warehouse(Warehouse.GenerateNewGuid(), Name, RegistredProducts);
    }
}
