using SimpleWebApp.Domain.Abstracts;

namespace SimpleWebApp.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }
        public List<KeyValuePair<KeyValuePair<Guid, string>, uint>> RegistredProducts { get; set; }

        public Warehouse() { }

        public Warehouse(Guid id, string name, List<KeyValuePair<KeyValuePair<Guid, string>, uint>> registredProducts)
            :base(id)
        {
            Name = name;
            RegistredProducts = registredProducts;
        }

        public static Guid GenerateNewGuid()
            => Guid.NewGuid();

        public void AddProducts(List<KeyValuePair<KeyValuePair<Guid, string>, uint>> addedProducts)
        {
            if (addedProducts == null || addedProducts.Count() == 0)
                return;

            RegistredProducts.AddRange(addedProducts);
        }

        public void RemoveProducts(List<KeyValuePair<KeyValuePair<Guid, string>, uint>> sellProducts)
        {
            if (sellProducts == null || sellProducts.Count() == 0)
                return;

            RegistredProducts.ForEach(p => 
            {
                if (sellProducts.Any(a => a.Key.Key == p.Key.Key && a.Value > p.Value))
                    throw new Exception("Зарзные приходы. Количество товаров в продаже превышает количество товаров на складе в одном приходе.");
                // обработка логики продажит товаров со склада. Можно сделать выборку из разных приходов объеденить все количество и продать.
            });
        }
    }
}
