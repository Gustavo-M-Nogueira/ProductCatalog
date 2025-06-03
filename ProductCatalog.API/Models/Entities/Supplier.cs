namespace ProductCatalog.API.Models.Entities
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
