using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Models.Entities;

namespace ProductCatalog.API.Data.Repositories
{
    public class CategoryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Categories.ToListAsync();
            }
        }

        public async Task<Category?> Find(int id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Categories.FindAsync(id);
            }
        }

        public async Task<Category> Create(Category category)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();

                return category;
            }
        }

        public async Task<Category> Update(Category category)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return category;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Category category = new Category()
                {
                    Id = id
                };

                context.Categories.Remove(category);

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
