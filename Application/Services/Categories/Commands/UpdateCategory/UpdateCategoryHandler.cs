using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler
        (ICategoryRepository categoryRepository)
        : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
    {
        public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            Category? category = await categoryRepository.Find(command.Id, cancellationToken);

            if (category is null)
                throw new Exception("Category not found");

            category.Title = command.Category.Title;

            var updatedCategory = await categoryRepository.Update(category, cancellationToken);

            var response = updatedCategory.Adapt<CategoryResponseDto>();

            return new UpdateCategoryResult(response);
        }
    }
}
