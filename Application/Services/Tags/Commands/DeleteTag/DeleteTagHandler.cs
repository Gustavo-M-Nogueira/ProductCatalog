using Application.Repositories;
using BuildingBlocks.CQRS;
using Tag = Domain.Entities.Tag;

namespace Application.Services.Tags.Commands.DeleteTag
{
    public class DeleteTagHandler
        (ITagRepository tagRepository)
        : ICommandHandler<DeleteTagCommand, DeleteTagResult>
    {
        public async Task<DeleteTagResult> Handle(DeleteTagCommand command, CancellationToken cancellationToken)
        {
            Tag? tag = await tagRepository.Find(command.Id, cancellationToken);

            if (tag is null)
                throw new Exception("Tag not found");

            var response = await tagRepository.Delete(command.Id, cancellationToken);

            return new DeleteTagResult(response);
        }
    }
}
