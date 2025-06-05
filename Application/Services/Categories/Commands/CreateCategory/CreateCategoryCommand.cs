using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(CategoryRequestDto Category) : ICommand<CreateCategoryResult>;
    public record CreateCategoryResult(CategoryResponseDto Category);
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand> 
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Category.Title).NotEmpty().WithMessage("Title cannot be empty.");
        }
    }
}
