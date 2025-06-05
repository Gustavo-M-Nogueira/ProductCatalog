using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using Domain.Entities;
using FluentValidation;

namespace Application.Services.Suppliers.Commands.CreateSupplier
{
    public record CreateSupplierCommand
        (Guid AddressId, SupplierRequestDto Supplier) 
        : ICommand<CreateSupplierResult>;
    public record CreateSupplierResult(SupplierResponseDto Supplier);
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(x => x.AddressId).NotEmpty().WithMessage("Address ID is required.");
            RuleFor(x => x.Supplier.Name).NotEmpty().WithMessage("Name cannot be empty.");
        }
    }
}
