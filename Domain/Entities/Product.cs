namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public List<ProductTag>? ProductTags { get; set; }
    }
}
