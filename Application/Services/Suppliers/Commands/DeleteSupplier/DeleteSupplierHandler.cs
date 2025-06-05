using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;

namespace Application.Services.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierHandler
        (ISupplierRepository supplierRepository)
        : ICommandHandler<DeleteSupplierCommand, DeleteSupplierResult>
    {
        public async Task<DeleteSupplierResult> Handle(DeleteSupplierCommand command, CancellationToken cancellationToken)
        {
            Supplier? supplier = await supplierRepository.Find(command.Id, cancellationToken);

            if (supplier is null)
                throw new Exception("Supplier not found");

            var response = await supplierRepository.Delete(command.Id, cancellationToken);

            return new DeleteSupplierResult(response);
        }
    }
}
