using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Addresses.Commands.DeleteAddress
{
    public record DeleteAddressCommand(Guid Id) : ICommand<DeleteAddressResult>;
    public record DeleteAddressResult(bool IsSuccess);
    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
    {
        public DeleteAddressCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
        }
    }
}
