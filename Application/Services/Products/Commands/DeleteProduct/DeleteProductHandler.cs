using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler
        (IProductRepository productRepository)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.Find(command.Id, cancellationToken);

            if (product is null)
                throw new Exception("Product not found");

            var response = await productRepository.Delete(command.Id, cancellationToken);
            
            return new DeleteProductResult(response);
        }
    }
}
