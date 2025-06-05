using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Products.Commands.UpdateProduct;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Product
{
    [ExtendObjectType(typeof(Mutation))]
    public class UpdateProductMutation
    {
        public record UpdateProductRequest(Guid Id, ProductRequestDto Product);
        public record UpdateProductResponse(ProductResponseDto Product);
        public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateProductCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateProductResponse>();

            return response;
        }
    }
}
