using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Mapster;
using Tag = Domain.Entities.Tag;

namespace Application.Services.Tags.Commands.UpdateTag
{
    public class UpdateTagHandler
        (ITagRepository tagRepository)
        : ICommandHandler<UpdateTagCommand, UpdateTagResult>
    {
        public async Task<UpdateTagResult> Handle(UpdateTagCommand command, CancellationToken cancellationToken)
        {
            Tag? tag = await tagRepository.Find(command.Id, cancellationToken);

            if (tag is null)
                throw new Exception("Tag not found");

            tag.Title = command.Tag.Title;

            var updatedTag = await tagRepository.Update(tag, cancellationToken);

            var response = updatedTag.Adapt<TagResponseDto>();

            return new UpdateTagResult(response);
        }
    }
}
