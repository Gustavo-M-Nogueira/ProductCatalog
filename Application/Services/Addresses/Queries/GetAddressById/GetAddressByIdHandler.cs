using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Addresses.Queries.GetAddressById
{
    public class GetAddressByIdHandler
        (IAddressRepository addressRepository)
        : IQueryHandler<GetAddressByIdQuery, GetAddressByIdResult>
    {
        public async Task<GetAddressByIdResult> Handle(GetAddressByIdQuery query, CancellationToken cancellationToken)
        {
            Address? address = await addressRepository.Find(query.Id, cancellationToken);

            if (address is null)
                throw new GraphQLException(new Error("Address not found", "ADDRESS_NOT_FOUND"));

            var response = address.Adapt<AddressResponseDto>();

            return new GetAddressByIdResult(response);
        }
    }
}
