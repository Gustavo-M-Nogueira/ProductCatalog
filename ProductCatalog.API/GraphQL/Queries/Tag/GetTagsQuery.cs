using Application.DTOs.Responses;
using Application.Services.Tags.Queries.GetTags;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Queries.Tag
{
    [ExtendObjectType(typeof(Query))]
    public class GetTagsGraphQlQuery
    {
        public record GetTagsResponse(IEnumerable<TagResponseDto> Tags);
        public async Task<GetTagsResponse> GetTags(ISender sender, CancellationToken cancellationToken)
        {
            var query = new GetTagsQuery();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetTagsResponse>();

            return response;
        }
    }
}
