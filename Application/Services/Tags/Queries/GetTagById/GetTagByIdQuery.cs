using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Tags.Queries.GetTagById
{
    public record GetTagByIdQuery(int Id) : IQuery<GetTagByIdResult>;
    public record GetTagByIdResult(TagResponseDto Tag);
    public class GetTagByIdQueryValidator : AbstractValidator<GetTagByIdQuery>
    {
        public GetTagByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required.");  
        }
    }
}
