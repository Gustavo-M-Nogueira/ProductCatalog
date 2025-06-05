using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Products.Queries.GetProductById
{ 
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ProductResponseDto Product);
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required.");
        }
    }
}
