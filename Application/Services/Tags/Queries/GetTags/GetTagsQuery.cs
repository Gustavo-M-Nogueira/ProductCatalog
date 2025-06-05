using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Tags.Queries.GetTags
{
    public record GetTagsQuery() : IQuery<GetTagsResult>;
    public record GetTagsResult(IEnumerable<TagResponseDto> Tags);
    public class GetTagsQueryValidator : AbstractValidator<GetTagsQuery>
    {
        public GetTagsQueryValidator()
        {
            
        }
    }
}
