using Application.DTOs.Responses;
using Application.Services.Categories.Queries.GetCategoryById;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Queries.Category
{
    [ExtendObjectType(typeof(Query))]
    public class GetCategoryByIdGraphQlQuery
    {
        public record GetCategoryByIdRequest(int Id);
        public record GetCategoryByIdResponse(CategoryResponseDto Category);
        public async Task<GetCategoryByIdResponse> GetCategoryById(GetCategoryByIdRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetCategoryByIdQuery>();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetCategoryByIdResponse>();

            return response;
        }
    }
}
