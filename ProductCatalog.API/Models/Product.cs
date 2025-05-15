namespace ProductCatalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int MyProperty { get; set; }
        public Forner Forner { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
