namespace ProductCatalog.API.Models.DTOs
{
    public class SupplierRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid AddressId { get; set; }
    }
}
