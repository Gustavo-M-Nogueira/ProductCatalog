using Application.DTOs.Responses;
using Application.Services.Suppliers.Queries.GetSupplierById;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Queries.Supplier
{
    [ExtendObjectType(typeof(Query))]
    public class GetSupplierByIdGraphQLQuery
    {
        public record GetSupplierByIdRequest(Guid Id);
        public record GetSupplierByIdResponse(SupplierResponseDto Supplier);
        public async Task<GetSupplierByIdResponse> GetSupplierById(GetSupplierByIdRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetSupplierByIdQuery>();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetSupplierByIdResponse>();

            return response;
        }
    }
}
