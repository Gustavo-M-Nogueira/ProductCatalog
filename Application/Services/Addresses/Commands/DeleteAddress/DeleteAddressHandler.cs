using System;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;


namespace Application.Services.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressHandler
        (IAddressRepository addressRepository) 
        : ICommandHandler<DeleteAddressCommand, DeleteAddressResult>
    {
        public async Task<DeleteAddressResult> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
        {
            Address? address = await addressRepository.Find(command.Id, cancellationToken);

            if (address is null)
                throw new Exception("Address not found");

            var response = await addressRepository.Delete(command.Id, cancellationToken);

            return new DeleteAddressResult(response);
        }
    }
}
