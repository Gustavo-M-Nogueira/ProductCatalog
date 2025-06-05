using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Products.Queries.GetProductById
{
    public class GetProductByIdHandler
        (IProductRepository productRepository)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.Find(query.Id, cancellationToken);

            if (product is null)
                throw new GraphQLException(new Error("Product not found", "PRODUCT_NOT_FOUND"));

            var response = product.Adapt<ProductResponseDto>();

            return new GetProductByIdResult(response);
        }
    }
}
