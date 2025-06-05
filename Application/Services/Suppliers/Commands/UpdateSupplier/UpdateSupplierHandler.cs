using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierHandler
        (ISupplierRepository supplierRepository)
        : ICommandHandler<UpdateSupplierCommand, UpdateSupplierResult>
    {
        public async Task<UpdateSupplierResult> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
        {
            Supplier? supplier = await supplierRepository.Find(command.Id, cancellationToken);

            if (supplier is null)
                throw new Exception("Supplier not found");

            supplier.Name = command.Supplier.Name;
            supplier.Description = command.Supplier.Description;
            supplier.AddressId = command.Supplier.AddressId;

            var updatedSupplier = await supplierRepository.Update(supplier, cancellationToken);

            var response = updatedSupplier.Adapt<SupplierResponseDto>();

            return new UpdateSupplierResult(response);
        }
    }
}
