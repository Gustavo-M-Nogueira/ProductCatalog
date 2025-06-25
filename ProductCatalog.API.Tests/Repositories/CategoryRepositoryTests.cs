using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Tests.InMemoryDb;

namespace ProductCatalog.API.Tests.Repositories
{
    public class CategoryRepositoryTests
    {
        private static readonly int _categoryQuantity = 5;
        private readonly CategoryDb _categoryDb;
        public static IEnumerable<object[]> ValidCategoryIds => Enumerable.Range(1, _categoryQuantity).Select(id => new object[] { id });
        public static IEnumerable<object[]> NewValidCategoryIds => Enumerable.Range(11, 10).Select(id => new object[] { id });
        public static IEnumerable<object[]> InvalidCategoryIds => Enumerable.Range(-10, 10).Select(id => new object[] { id });

        public CategoryRepositoryTests()
        {
            _categoryDb = new CategoryDb(_categoryQuantity);
        }

        [Fact]
        public async Task CategoryRepository_GetAll()
        {
            var categoryRepository = await _categoryDb.GetCategoryRepository();

            var result = await categoryRepository.GetAll();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }


        [Theory]
        [MemberData(nameof(ValidCategoryIds))]
        public async Task CategoryRepository_Find_ShouldFindById(int id)
        {
            var categoryRepository = await _categoryDb.GetCategoryRepository();

            var result = await categoryRepository.Find(id);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(id, result.Id);
        }


        [Theory]
        [MemberData(nameof(InvalidCategoryIds))]
        public async Task CategoryRepository_Find_ShouldNotFindWrongId(int id)
        {
            var categoryRepository = await _categoryDb.GetCategoryRepository();

            var result = await categoryRepository.Find(id);

            Assert.Null(result);
        }


        [Theory]
        [InlineData(6, "New Title")]
        public async Task CategoryRepository_Create(int id, string title)
        {
            var category = new Category();
            category.Id = id;
            category.Title = title;
            var categoryRepository = await _categoryDb.GetCategoryRepository();

            var result = await categoryRepository.Create(category);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(title, result.Title);
        }


        [Theory]
        [InlineData(1, "New Title")]
        public async Task CategoryRepository_Update(int id, string newTitle)
        {
            var dbContext = await _categoryDb.GetCategoryDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            var categoryRepository = new CategoryRepository(fakeDbFactory);

            var category = await dbContext.Categories.FirstAsync(t => t.Id == id);
            
            string oldTitle = category.Title;
            
            category.Title = newTitle;
            
            var result = await categoryRepository.Update(category);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(oldTitle, result.Title);
            Assert.Equal(newTitle, result.Title);
        }

        [Theory]
        [MemberData(nameof(ValidCategoryIds))]
        public async Task CategoryRepository_Delete_ShouldWork(int id)
        {
            var categoryRepository = await _categoryDb.GetCategoryRepository();

            bool result = await categoryRepository.Delete(id);

            Assert.True(result);
        }


        [Theory]
        [MemberData(nameof(InvalidCategoryIds))]
        [MemberData(nameof(NewValidCategoryIds))]
        public async Task CategoriesRepository_Delete_ShouldNotFound(int id)
        {
            var categoryRepository = await _categoryDb.GetCategoryRepository();

            bool result = await categoryRepository.Delete(id);

            Assert.False(result);
        }
    }
}
