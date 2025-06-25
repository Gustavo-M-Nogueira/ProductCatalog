using Application.DTOs.Requests;
using Application.Repositories;
using Application.Services.Categories.Commands.CreateCategory;
using Application.Services.Categories.Commands.UpdateCategory;
using Domain.Entities;
using Moq;
using ProductCatalog.API.Tests.InMemoryDb;

namespace ProductCatalog.API.Tests.Handlers.Commands
{
    public class CategoryCommandHandlerTests
    {
        private readonly static int _categoryQuantity = 5;
        private readonly CategoryDb _categoryDb;

        public CategoryCommandHandlerTests()
        {
            _categoryDb = new CategoryDb(_categoryQuantity);
        }

        [Theory]
        [InlineData("New Title")]
        public async Task CreateCategoryHandler_ShouldCreate(string title)
        {
            var category = new CategoryRequestDto { Title = title };
            var command = new CreateCategoryCommand(category);
            var categoryRepository = await _categoryDb.GetCategoryRepository();
            var handler = new CreateCategoryHandler(categoryRepository);

            var result = await handler.Handle(command, default);

            Assert.IsType<CreateCategoryResult>(result);
            Assert.NotNull(result);
            Assert.True(result.Category.Id > 0);
            Assert.True(result.Category.Title == category.Title);
            Assert.True(title == result.Category.Title);
        }

        [Theory]
        [InlineData(1, "Updated Title")]
        [InlineData(2, "New Updated Title")]
        public async Task UpdateCategoryHandler_ShouldCreate(int id, string title)
        {
            // Arrange
            var existingCategory = new Category { Id = id, Title = "Old Title" };

            var mockRepo = new Mock<ICategoryRepository>();

            // Mock Find
            mockRepo.Setup(r => r.Find(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingCategory);

            // Mock Update
            mockRepo.Setup(r => r.Update(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category c, CancellationToken _) => new Category
                {
                    Id = c.Id,
                    Title = c.Title
                });

            var command = new UpdateCategoryCommand(id, new CategoryRequestDto { Title = title });
            var handler = new UpdateCategoryHandler(mockRepo.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Category);
            Assert.Equal(id, result.Category.Id);
            Assert.Equal(title, result.Category.Title);
        }
    }
}
