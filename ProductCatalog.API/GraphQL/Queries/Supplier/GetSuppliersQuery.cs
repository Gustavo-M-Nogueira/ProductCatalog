using Application.DTOs.Responses;
using Application.Services.Suppliers.Queries.GetSuppliers;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Queries.Supplier
{
    [ExtendObjectType(typeof(Query))]
    public class GetSuppliersGraphQlQuery
    {
        public record GetSuppliersResponse(IEnumerable<SupplierResponseDto> Suppliers);
        public async Task<GetSuppliersResponse> GetSuppliers(ISender sender, CancellationToken cancellationToken)
        {
            var query = new GetSuppliersQuery();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetSuppliersResponse>();

            return response;
        }
    }
}
