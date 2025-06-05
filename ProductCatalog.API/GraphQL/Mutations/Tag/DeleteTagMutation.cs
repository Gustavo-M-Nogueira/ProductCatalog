using Application.Services.Tags.Commands.DeleteTag;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Tag
{
    [ExtendObjectType(typeof(Mutation))]
    public class DeleteTagMutation
    {
        public record DeleteTagRequest(int Id);
        public record DeleteTagResponse(bool IsSuccess);
        public async Task<DeleteTagResponse> DeleteTag(DeleteTagRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<DeleteTagCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<DeleteTagResponse>();

            return response;
        }
    }
}
