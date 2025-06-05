using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Products.Commands.CreateProduct;
using HotChocolate.Types;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Product
{
    [ExtendObjectType(typeof(Mutation))]
    public class CreateProductMutation
    {
        public record CreateProductRequest(ProductRequestDto Product);
        public record CreateProductResponse(ProductResponseDto Product);
        public async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateProductResponse>();

            return response;
        }
    }
}
