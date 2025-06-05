using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;
using Tag = Domain.Entities.Tag;

namespace Application.Services.Tags.Commands.CreateTag
{
    public class CreateTagHandler
        (ITagRepository tagRepository)
        : ICommandHandler<CreateTagCommand, CreateTagResult>
    {
        public async Task<CreateTagResult> Handle(CreateTagCommand command, CancellationToken cancellationToken)
        {
            Tag tag = command.Tag.Adapt<Tag>();

            var updatedTag = await tagRepository.Create(tag, cancellationToken);

            var response = updatedTag.Adapt<TagResponseDto>();
            
            return new CreateTagResult(response);
        }
    }
}
