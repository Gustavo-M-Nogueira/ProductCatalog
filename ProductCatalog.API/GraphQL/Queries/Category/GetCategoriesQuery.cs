using Application.DTOs.Responses;
using Application.Services.Categories.Queries.GetCategories;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Queries.Category
{
    [ExtendObjectType(typeof(Query))]
    public class GetCategoriesGraphQLQuery
    {
        public record GetCategoriesResponse(IEnumerable<CategoryResponseDto> Categories);
        public async Task<GetCategoriesResponse> GetCategories(ISender sender, CancellationToken cancellationToken)
        {
            var query = new GetCategoriesQuery();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetCategoriesResponse>();

            return response;
        }
    }
}
