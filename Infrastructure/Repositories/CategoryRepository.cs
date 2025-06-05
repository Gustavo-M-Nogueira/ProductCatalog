using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Categories.ToListAsync(cancellationToken);
            }
        }

        public async Task<Category?> Find(int id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Categories.FindAsync(id, cancellationToken);
            }
        }

        public async Task<Category> Create(Category category, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync(cancellationToken);

                return category;
            }
        }

        public async Task<Category> Update(Category category, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Categories.Update(category);
                await context.SaveChangesAsync(cancellationToken);

                return category;
            }
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Category category = new Category()
                {
                    Id = id
                };

                context.Categories.Remove(category);

                return await context.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
