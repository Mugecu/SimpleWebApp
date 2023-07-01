﻿using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.Guards;

namespace SimpleWebApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; private set; }
        public uint ItemsNumber { get; private set; }
        public decimal PurchasePrice { get; private set; }
        public decimal SellPrice { get; private set; }

        public Product(
            Guid id,
            string productName,
            uint itemsNumber,
            decimal purchasePrice,
            decimal sellPrice) 
            : base(id)
        {
            Check.CheckStringOnNullOrWhiteSpase(productName, "Родукт обязательно должен содержать название.");
            Check.CheckPriceOnNegativeValue(purchasePrice, "Цена покупи должна быть положительная.");
            Check.CheckPriceOnNegativeValue(sellPrice, "Цена продажи должна быть положительная.");

            ProductName = productName;
            ItemsNumber = itemsNumber;
            PurchasePrice = purchasePrice;
            SellPrice = sellPrice;
        }
    }
}