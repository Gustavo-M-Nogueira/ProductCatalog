using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Tags.Commands.UpdateTag
{
    public record UpdateTagCommand
        (int Id, TagRequestDto Tag) 
        : ICommand<UpdateTagResult>;

    public record UpdateTagResult(TagResponseDto Tag);

    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
            RuleFor(x => x.Tag.Title).NotEmpty().WithMessage("Title cannot be empty");
        }
    }
}
