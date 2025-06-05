using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Suppliers.Commands.DeleteSupplier
{
    public record DeleteSupplierCommand(Guid Id) : ICommand<DeleteSupplierResult>;
    public record DeleteSupplierResult(bool IsSuccess);
    public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
    {
        public DeleteSupplierCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
        }
    }
}
