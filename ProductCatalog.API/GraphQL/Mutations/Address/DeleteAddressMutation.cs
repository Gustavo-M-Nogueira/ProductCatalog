using Application.Services.Addresses.Commands.DeleteAddress;
using HotChocolate.Types;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Address
{
    [ExtendObjectType(typeof(Mutation))]
    public class DeleteAddressMutation
    {
        public record DeleteAddressRequest(Guid Id);
        public record DeleteAddressResponse(bool IsSuccess);
        public async Task<DeleteAddressResponse> DeleteAddress(DeleteAddressRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<DeleteAddressCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<DeleteAddressResponse>();

            return response;
        }
    }
}
