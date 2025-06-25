using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.API.Tests.InMemoryDb
{
    public class InMemoryDatabase
    {
        public async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }

    public class FakeDbContextFactory(ApplicationDbContext context) : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext()
        {
            return context;
        }
    }
}
