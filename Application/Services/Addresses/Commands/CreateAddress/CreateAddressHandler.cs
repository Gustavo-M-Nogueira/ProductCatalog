using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Addresses.Commands.CreateAddress
{
    public class CreateAddressHandler(IAddressRepository addressRepository) 
        : ICommandHandler<CreateAddressCommand, CreateAddressResult>
    {
        public async Task<CreateAddressResult> Handle(CreateAddressCommand command, CancellationToken cancellationToken)
        {
            Address address = command.Address.Adapt<Address>();

            var updatedAddress = await addressRepository.Create(address, cancellationToken);

            var response = updatedAddress.Adapt<AddressResponseDto>();

            return new CreateAddressResult(response);
        }
    }
}