using Application.Repositories;
using BuildingBlocks.CQRS;
using Domain.Entities;

namespace Application.Services.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryHandler
        (ICategoryRepository categoryRepository)
        : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
    {
        public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            Category? category = await categoryRepository.Find(command.Id, cancellationToken);

            if (category is null)
                throw new Exception("Category not found");

            var response = await categoryRepository.Delete(command.Id, cancellationToken);

            return new DeleteCategoryResult(response);
        }
    }
}
