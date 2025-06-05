using Application.DTOs.Responses;
using Application.Services.Products.Queries.GetProducts;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Queries.Product
{
    [ExtendObjectType(typeof(Query))]
    public class GetProductsGraphQLQuery
    {
        public record GetProductsResponse(IEnumerable<ProductResponseDto> Products);
        public async Task<GetProductsResponse> GetProducts(ISender sender, CancellationToken cancellationToken)
        {
            var query = new GetProductsQuery();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetProductsResponse>();

            return response;
        }
    }
}
