using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(int Id) : ICommand<DeleteCategoryResult>;
    public record DeleteCategoryResult(bool IsSuccess);
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required.");
        }
    }
}
