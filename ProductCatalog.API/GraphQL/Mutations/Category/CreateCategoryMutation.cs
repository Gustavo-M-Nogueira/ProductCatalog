using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Categories.Commands.CreateCategory;
using HotChocolate.Types;
using Mapster;
using MediatR;

namespace ProductCatalog.API.GraphQL.Mutations.Category
{
    [ExtendObjectType(typeof(Mutation))]
    public class CreateCategoryMutation
    {
        public record CreateCategoryRequest(CategoryRequestDto Category);
        public record CreateCategoryResponse(CategoryResponseDto Category);
        public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request, ISender sender, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateCategoryCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateCategoryResponse>();

            return response;
        }
    }
}
