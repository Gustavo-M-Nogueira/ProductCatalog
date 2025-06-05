using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressHandler
        (IAddressRepository addressRepository) 
        : ICommandHandler<UpdateAddressCommand, UpdateAddressResult>
    {
        public async Task<UpdateAddressResult> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            Address? address = await addressRepository.Find(command.Id, cancellationToken);

            if (address is null)
                throw new Exception("Address not found");

            address.AddressLine = command.Address.AddressLine;
            address.Country = command.Address.Country;
            address.State = command.Address.State;
            address.ZipCode = command.Address.ZipCode;

            var updatedAddress = await addressRepository.Update(address, cancellationToken);

            var response = updatedAddress.Adapt<AddressResponseDto>();

            return new UpdateAddressResult(response);
        }
    }
}
