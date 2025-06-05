using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Tags.Commands.CreateTag;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Tag
{
    [ExtendObjectType(typeof(Mutation))]
    public class CreateTagMutation
    {
        public record CreateTagRequest(TagRequestDto Tag);
        public record CreateTagResponse(TagResponseDto Tag);
        public async Task<CreateTagResponse> CreateTag(CreateTagRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateTagCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateTagResponse>();

            return response;
        }
    }
}
