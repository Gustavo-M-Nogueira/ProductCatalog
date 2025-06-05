using Application.DTOs.Responses;
using Application.Services.Products.Queries.GetProductById;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Queries.Product
{
    [ExtendObjectType(typeof(Query))]
    public class GetProductByIdGraphQlQuery
    {
        public record GetProductByIdRequest(Guid Id);
        public record GetProductByIdResponse(ProductResponseDto Product);
        public async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetProductByIdQuery>();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetProductByIdResponse>();

            return response;
        }
    }
}
