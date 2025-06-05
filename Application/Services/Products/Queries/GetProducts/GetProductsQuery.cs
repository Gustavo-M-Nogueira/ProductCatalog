using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Products.Queries.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ProductResponseDto> Products);
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            
        }
    }
}
