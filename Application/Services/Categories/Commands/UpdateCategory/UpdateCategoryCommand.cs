using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand
        (int Id, CategoryRequestDto Category) 
        : ICommand<UpdateCategoryResult>;
    public record UpdateCategoryResult(CategoryResponseDto Category);
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
            RuleFor(x => x.Category.Title).NotEmpty().WithMessage("Title is required");
        }
    }
}
