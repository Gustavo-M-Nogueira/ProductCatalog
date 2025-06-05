using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Addresses.Queries.GetAddresses
{
    public class GetAddressesHandler
        (IAddressRepository addressRepository)
        : IQueryHandler<GetAddressesQuery, GetAddressesResult>
    {
        public async Task<GetAddressesResult> Handle(GetAddressesQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Address> addresses = await addressRepository.GetAll(cancellationToken);

            var response = addresses.Select(a => a.Adapt<AddressResponseDto>());
            
            return new GetAddressesResult(response);
        }
    }
}
