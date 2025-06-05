using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Categories.Queries.GetCategories
{
    public class GetCategoriesHandler
        (ICategoryRepository categoryRepository)
        : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Category> categories = await categoryRepository.GetAll(cancellationToken);

            var response = categories.Select(c => c.Adapt<CategoryResponseDto>());

            return new GetCategoriesResult(response);
        }
    }
}
