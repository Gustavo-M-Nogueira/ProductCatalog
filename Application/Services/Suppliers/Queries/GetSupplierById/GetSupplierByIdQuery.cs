using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Suppliers.Queries.GetSupplierById
{
    public record GetSupplierByIdQuery(Guid Id) : IQuery<GetSupplierByIdResult>;
    public record GetSupplierByIdResult(SupplierResponseDto Supplier);
    public class GetSupplierByIdQueryValidator : AbstractValidator<GetSupplierByIdQuery>
    {
        public GetSupplierByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required.");
        }
    }
}
