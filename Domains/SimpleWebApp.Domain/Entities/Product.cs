using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.Guards;

namespace SimpleWebApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; private set; }
        public uint ProductNumber { get; private set; }
        public decimal PurchasePrice { get; private set; }
        public decimal SellPrice { get; private set; }

        public Product(
            Guid id,
            string productName,
            uint productNumber,
            decimal purchasePrice,
            decimal sellPrice) 
            : base(id)
        {
            Check.StringOnNullOrWhiteSpase(productName, "Родукт обязательно должен содержать название.");
            Check.PriceOnNegativeValue(purchasePrice, "Цена покупи должна быть положительная.");
            Check.PriceOnNegativeValue(sellPrice, "Цена продажи должна быть положительная.");

            ProductName = productName;
            ProductNumber = productNumber;
            PurchasePrice = purchasePrice;
            SellPrice = sellPrice;
        }

        public static Guid GenerateNewGuid()
            => Guid.NewGuid();

        public Product Update(Product productUpdateParameter)
        {
            SellPrice= productUpdateParameter.SellPrice;
            return this;
        }
    }
}