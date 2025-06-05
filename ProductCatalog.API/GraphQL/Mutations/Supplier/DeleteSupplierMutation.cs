using Application.Services.Suppliers.Commands.DeleteSupplier;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Supplier
{
    [ExtendObjectType(typeof(Mutation))]
    public class DeleteSupplierMutation
    {
        public record DeleteSupplierRequest(Guid Id);
        public record DeleteSupplierResponse(bool IsSuccess);
        public async Task<DeleteSupplierResponse> DeleteSupplier(DeleteSupplierRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<DeleteSupplierCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<DeleteSupplierResponse>();

            return response;
        }
    }
}
