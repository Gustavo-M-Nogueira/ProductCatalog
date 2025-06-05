using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Suppliers.Commands.UpdateSupplier
{
    public record UpdateSupplierCommand(
        Guid Id,
        SupplierRequestDto Supplier) 
        : ICommand<UpdateSupplierResult>;
    public record UpdateSupplierResult(SupplierResponseDto Supplier);
    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty.");
            RuleFor(x => x.Supplier.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Supplier.AddressId).NotEmpty().WithMessage("Address ID is required.");
        }
    }
}
