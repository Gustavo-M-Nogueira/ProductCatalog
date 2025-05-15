namespace ProductCatalog.API.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;
    }
}
