using Mapster;
using MediatR;
using Application.DTOs.Responses;
using Application.Services.Addresses.Queries.GetAddressById;

namespace ProductCatalog.API.GraphQL.Queries.Address
{
    [ExtendObjectType(typeof(Query))]
    public class GetAddressByIdGraphQLQuery
    {
        public record GetAddressByIdRequest(Guid Id);
        public record GetAddressByIdResponse(AddressResponseDto Address);
        public async Task<GetAddressByIdResponse> GetAddressById(GetAddressByIdRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetAddressByIdQuery>();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetAddressByIdResponse>();

            return response;
        }
    }
}
