using Application.DTOs.Responses;
using Application.Services.Tags.Queries.GetTagById;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Queries.Tag
{
    [ExtendObjectType(typeof(Query))]
    public class GetTagByIdGraphQLQuery
    {
        public record GetTagByIdRequest(int Id);
        public record GetTagByIdResponse(TagResponseDto Tag);
        public async Task<GetTagByIdResponse> GetTagById(GetTagByIdRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetTagByIdQuery>();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetTagByIdResponse>();

            return response;
        }
    }
}
