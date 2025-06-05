using Application.Services.Products.Commands.DeleteProduct;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Product
{
    [ExtendObjectType(typeof(Mutation))]
    public class DeleteProductMutation
    {
        public record DeleteProductRequest(Guid Id);
        public record DeleteProductResponse(bool IsSuccess);
        public async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<DeleteProductCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<DeleteProductResponse>();

            return response;
        }
    }
}
