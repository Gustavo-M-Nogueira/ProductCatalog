using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Suppliers.Commands.CreateSupplier;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Supplier
{
    [ExtendObjectType(typeof(Mutation))]
    public class CreateSupplierMutation
    {
        public record CreateSupplierRequest(SupplierRequestDto Supplier);
        public record CreateSupplierResponse(SupplierResponseDto Supplier);
        public async Task<CreateSupplierResponse> CreateSupplier(CreateSupplierRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateSupplierCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateSupplierResponse>();

            return response;
        }
    }
}
