using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery() : IQuery<GetCategoriesResult>;
    public record GetCategoriesResult(IEnumerable<CategoryResponseDto> Categories);
    public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        {
            
        }
    }
}
