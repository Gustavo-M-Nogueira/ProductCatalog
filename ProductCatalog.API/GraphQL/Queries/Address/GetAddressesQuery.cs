using Mapster;
using MediatR;
using Application.DTOs.Responses;
using Application.Services.Addresses.Queries.GetAddresses;
using HotChocolate.Types;

namespace ProductCatalog.API.GraphQL.Queries.Address
{
    [ExtendObjectType(typeof(Query))]
    public class GetAddressesGraphQLQuery
    {
        public record GetAddressesResponse(IEnumerable<AddressResponseDto> Addresses);
        public async Task<GetAddressesResponse> GetAddresses(ISender sender, CancellationToken cancellationToken)
        {
            var query = new GetAddressesQuery();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetAddressesResponse>();

            return response;
        }
    }
}
