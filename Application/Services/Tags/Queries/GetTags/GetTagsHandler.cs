using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Mapster;
using Tag = Domain.Entities.Tag;

namespace Application.Services.Tags.Queries.GetTags
{
    public class GetTagsHandler
        (ITagRepository tagRepository)
        : IQueryHandler<GetTagsQuery, GetTagsResult>
    {
        public async Task<GetTagsResult> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Tag> tags = await tagRepository.GetAll(cancellationToken);

            var response = tags.Select(t => t.Adapt<TagResponseDto>());

            return new GetTagsResult(response);
        }
    }
}
