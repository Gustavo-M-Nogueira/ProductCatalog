using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Mapster;
using Tag = Domain.Entities.Tag;

namespace Application.Services.Tags.Queries.GetTagById
{
    public class GetTagByIdHandler
        (ITagRepository tagRepository)
        : IQueryHandler<GetTagByIdQuery, GetTagByIdResult>
    {
        public async Task<GetTagByIdResult> Handle(GetTagByIdQuery query, CancellationToken cancellationToken)
        {
            Tag? tag = await tagRepository.Find(query.Id, cancellationToken);

            if (tag is null)
                throw new GraphQLException(new Error("Tag not found", "TAG_NOT_FOUND"));

            var response = tag.Adapt<TagResponseDto>();

            return new GetTagByIdResult(response);
        }
    }
}
