using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.API.Tests.InMemoryDb
{
    public class CategoryDb
    {
        private int _categoryQuantity;
        private readonly InMemoryDatabase _inMemoryDb;

        public CategoryDb(int categoryQuantity)
        {
            _inMemoryDb = new InMemoryDatabase();
            _categoryQuantity = categoryQuantity;
        }

        public async Task<CategoryRepository> GetCategoryRepository()
        {
            var dbContext = await GetCategoryDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new CategoryRepository(fakeDbFactory);
        }

        public async Task<ApplicationDbContext> GetCategoryDatabaseContext()
        {
            var dbContext = await _inMemoryDb.GetDatabaseContext();

            if (await dbContext.Categories.CountAsync() <= 0)
            {
                for (int i = 0; i < _categoryQuantity; i++)
                {
                    dbContext.Categories.Add(
                    new Category()
                    {
                        Title = $"Category {i}"
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
            return dbContext;
        }
    }
}
