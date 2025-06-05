using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Tags.Commands.DeleteTag
{
    public record DeleteTagCommand(int Id) : ICommand<DeleteTagResult>;
    public record DeleteTagResult(bool IsSuccess);
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required.");
        }
    }
}
