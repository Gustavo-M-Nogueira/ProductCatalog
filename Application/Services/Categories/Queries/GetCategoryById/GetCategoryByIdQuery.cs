using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(int Id) : IQuery<GetCategoryByIdResult>;
    public record GetCategoryByIdResult(CategoryResponseDto Category);
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
        }
    }
}
