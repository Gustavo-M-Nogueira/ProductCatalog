using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Addresses.Commands.CreateAddress;
using HotChocolate.Types;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Address
{
    public record CreateAddressRequest(AddressRequestDto Address);
    public record CreateAddressResponse(AddressResponseDto Address);

    [ExtendObjectType(typeof(Mutation))]
    public class CreateAddressMutation
    {
        public async Task<CreateAddressResponse> CreateAddress(CreateAddressRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateAddressCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateAddressResponse>();

            return response;
        }
    }
}
