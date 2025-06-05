using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Suppliers.Queries.GetSuppliers
{
    public class GetSuppliersHandler
        (ISupplierRepository supplierRepository)
        : IQueryHandler<GetSuppliersQuery, GetSuppliersResult>
    {
        public async Task<GetSuppliersResult> Handle(GetSuppliersQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Supplier> suppliers = await supplierRepository.GetAll(cancellationToken);

            var response = suppliers.Select(s => s.Adapt<SupplierResponseDto>());

            return new GetSuppliersResult(response);
        }
    }
}
