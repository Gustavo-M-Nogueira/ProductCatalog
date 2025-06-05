using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Addresses.Commands.UpdateAddress;
using HotChocolate.Types;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Address
{
    [ExtendObjectType(typeof(Mutation))]
    public class UpdateAddressMutation
    {
        public record UpdateAddressRequest(Guid Id, AddressRequestDto Address);
        public record UpdateAddressResponse(AddressResponseDto Address);
        public async Task<UpdateAddressResponse> UpdateAddress(UpdateAddressRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateAddressCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateAddressResponse>();

            return response;
        }
    }
}
