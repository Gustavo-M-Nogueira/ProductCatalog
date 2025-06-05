using Application.DTOs.Responses;
using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;
using Mapster;

namespace Application.Services.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler
        (ICategoryRepository categoryRepository)
        : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            Category category = command.Category.Adapt<Category>();

            var updatedCategory = await categoryRepository.Create(category, cancellationToken);

            var response = updatedCategory.Adapt<CategoryResponseDto>();

            return new CreateCategoryResult(response);
        }
    }
}
