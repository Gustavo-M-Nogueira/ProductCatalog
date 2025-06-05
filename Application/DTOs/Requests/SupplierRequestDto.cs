namespace Application.DTOs.Requests
{
    public class SupplierRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid AddressId { get; set; }
    }
}
