namespace Application.DTOs.Responses
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public Guid SupplierId { get; set; }
        public List<int>? TagIds { get; set; }
    }
}
