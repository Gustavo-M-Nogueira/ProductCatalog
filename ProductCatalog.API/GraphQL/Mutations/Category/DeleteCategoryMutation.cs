using Application.Services.Categories.Commands.DeleteCategory;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Category
{
    [ExtendObjectType(typeof(Mutation))]
    public class DeleteCategoryMutation
    {
        public record DeleteCategoryRequest(int Id);
        public record DeleteCategoryResponse(bool IsSuccess);
        public async Task<DeleteCategoryResponse> DeleteCategory(DeleteCategoryRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<DeleteCategoryCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<DeleteCategoryResponse>();

            return response;
        }
    }
}
