using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Products.Commands.CreateProduct
{
    public record CreateProductCommand
        (ProductRequestDto Product) 
        : ICommand<CreateProductResult>;

    public record CreateProductResult(ProductResponseDto Product);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Title).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(x => x.Product.Price).NotEmpty().WithMessage("Price cannot be empty.");
            RuleFor(x => x.Product.StockQuantity).NotEmpty().WithMessage("Stock quantity cannot be empty.");
            RuleFor(x => x.Product.CategoryId).NotEmpty().WithMessage("Category ID cannot be empty.");
            RuleFor(x => x.Product.SupplierId).NotEmpty().WithMessage("Supplier ID cannot be empty.");
        }
    }
}
