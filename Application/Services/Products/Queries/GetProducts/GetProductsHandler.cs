using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Products.Queries.GetProducts
{
    public class GetProductsHandler
        (IProductRepository productRepository)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await productRepository.GetAll(cancellationToken);

            var response = products.Select(p => p.Adapt<ProductResponseDto>());

            return new GetProductsResult(response);
        }
    }
}
