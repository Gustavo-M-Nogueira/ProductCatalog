using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using Domain.Entities;
using FluentValidation;

namespace Application.Services.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand
        (Guid Id, Product Product) 
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(ProductResponseDto Product);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required.");
            RuleFor(x => x.Product.Title).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(x => x.Product.Price).NotEmpty().WithMessage("Price cannot be empty.");
            RuleFor(x => x.Product.StockQuantity).NotEmpty().WithMessage("Stock quantity cannot be empty.");
            RuleFor(x => x.Product.CategoryId).NotEmpty().WithMessage("Category ID cannot be empty.");
            RuleFor(x => x.Product.SupplierId).NotEmpty().WithMessage("Supplier ID cannot be empty.");
        }
    }
}
