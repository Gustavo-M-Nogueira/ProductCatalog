namespace ProductCatalog.API.Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public List<ProductTag>? ProductTags { get; set; }
    }
}
