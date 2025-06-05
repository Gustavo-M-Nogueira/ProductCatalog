namespace Application.DTOs.Responses
{
    public class SupplierResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid AddressId { get; set; }
    }
}
