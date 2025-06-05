using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Products.Commands.CreateProduct
{
    public class CreateProductHandler
        (IProductRepository productRepository)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            Product product = command.Product.Adapt<Product>();

            var updatedProduct = await productRepository.Create(product, cancellationToken);

            var response = updatedProduct.Adapt<ProductResponseDto>();

            return new CreateProductResult(response);
        }
    }
}
