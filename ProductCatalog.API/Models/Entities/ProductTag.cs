namespace ProductCatalog.API.Models.Entities
{
    public class ProductTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
