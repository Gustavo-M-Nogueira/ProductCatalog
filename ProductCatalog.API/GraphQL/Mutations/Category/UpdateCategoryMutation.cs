using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Categories.Commands.UpdateCategory;
using HotChocolate.Types;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Category
{
    [ExtendObjectType(typeof(Mutation))]
    public class UpdateCategoryMutation
    {
        public record UpdateCategoryRequest(int Id, CategoryRequestDto Category);
        public record UpdateCategoryResponse(CategoryResponseDto Category);
        public async Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateCategoryCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<UpdateCategoryResponse>();

            return response;
        }
    }
}
