namespace Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string AddressLine { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
    }
}
