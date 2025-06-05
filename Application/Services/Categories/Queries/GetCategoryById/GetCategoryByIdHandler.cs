using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler
        (ICategoryRepository categoryRepository)
        : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdResult>
    {
        public async Task<GetCategoryByIdResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            Category? category = await categoryRepository.Find(query.Id, cancellationToken);

            if (category is null)
                throw new GraphQLException(new Error("Category not found", "CATEGORY_NOT_FOUND"));

            var response = category.Adapt<CategoryResponseDto>();

            return new GetCategoryByIdResult(response);
        }
    }
}
