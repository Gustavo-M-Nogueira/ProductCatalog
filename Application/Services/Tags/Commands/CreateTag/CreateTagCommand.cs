using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using Domain.Entities;
using FluentValidation;

namespace Application.Services.Tags.Commands.CreateTag
{
    public record CreateTagCommand(TagRequestDto Tag) : ICommand<CreateTagResult>;
    public record CreateTagResult(TagResponseDto Tag);
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(x => x.Tag.Title).NotEmpty().WithMessage("Title cannot be empty");
        }
    }
}
