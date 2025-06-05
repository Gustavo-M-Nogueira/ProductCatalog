using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Suppliers.Commands.UpdateSupplier;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Supplier
{
    [ExtendObjectType(typeof(Mutation))]
    public class UpdateSupplierMutation
    {
        public record UpdateSupplierRequest(Guid Id, SupplierRequestDto Supplier);
        public record UpdateSupplierResponse(SupplierResponseDto Supplier);
        public async Task<UpdateSupplierResponse> UpdateSupplier(UpdateSupplierRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateSupplierCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateSupplierResponse>();

            return response;
        }
    }
}
