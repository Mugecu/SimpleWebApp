using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Domain.DTOs
{
    public class ProductDTO
    {
        public Guid? Id { get; set; }
        public string ProductName { get; set; }
        public uint ProductNumber { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellPrice { get; set; }

        public ProductDTO ToDto(Product model)
        {
            if (model == null)
                return default;

            Id = model.Id;
            ProductName = model.ProductName;
            PurchasePrice = model.PurchasePrice;
            SellPrice = model.SellPrice;

            return this;
        }

        public Product ToModel()
            => Id.HasValue
                ? new Product(Id.Value, ProductName, ProductNumber, PurchasePrice, SellPrice)
                : new Product(Product.GenerateNewGuid(), ProductName, ProductNumber, PurchasePrice, SellPrice); 
    }
}
