using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler
        (IProductRepository productRepository)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.Find(command.Id, cancellationToken);

            if (product is null)
                throw new GraphQLException(new Error("Product not found", "PRODUCT_NOT_FOUND"));

            product.Title = command.Product.Title;
            product.Price = command.Product.Price;
            product.Description = command.Product.Description;
            product.StockQuantity = command.Product.StockQuantity;
            product.CategoryId = command.Product.CategoryId;
            product.SupplierId = command.Product.SupplierId;

            var updatedProduct = await productRepository.Update(product, cancellationToken);

            var response = updatedProduct.Adapt<ProductResponseDto>();
            
            return new UpdateProductResult(response);
        }
    }
}
