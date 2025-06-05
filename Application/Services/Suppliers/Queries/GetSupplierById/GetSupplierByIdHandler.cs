using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Suppliers.Queries.GetSupplierById
{
    public class GetSupplierByIdHandler
        (ISupplierRepository supplierRepository)
        : IQueryHandler<GetSupplierByIdQuery, GetSupplierByIdResult>
    {
        public async Task<GetSupplierByIdResult> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        {
            Supplier? supplier = await supplierRepository.Find(query.Id, cancellationToken);

            if (supplier is null)
                throw new GraphQLException(new Error("Supplier not found", "SUPPLIER_NOT_FOUND"));

            var response = supplier.Adapt<SupplierResponseDto>();

            return new GetSupplierByIdResult(response);
        }
    }
}
