using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierHandler
        (ISupplierRepository supplierRepository)
        : ICommandHandler<CreateSupplierCommand, CreateSupplierResult>
    {
        public async Task<CreateSupplierResult> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
        {
            Supplier supplier = command.Supplier.Adapt<Supplier>();

            var updatedSupplier = await supplierRepository.Create(supplier, cancellationToken);

            var response = updatedSupplier.Adapt<SupplierResponseDto>();

            return new CreateSupplierResult(response);
        }
    }
}
