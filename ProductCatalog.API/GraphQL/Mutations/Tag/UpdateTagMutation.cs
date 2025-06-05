using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Tags.Commands.UpdateTag;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Tag
{
    [ExtendObjectType(typeof(Mutation))]
    public class UpdateTagMutation
    {
        public record UpdateTagRequest(int Id, TagRequestDto Tag);
        public record UpdateTagResponse(TagResponseDto Tag);
        public async Task<UpdateTagResponse> UpdateTag(UpdateTagRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateTagCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateTagResponse>();

            return response;
        }
    }
}
