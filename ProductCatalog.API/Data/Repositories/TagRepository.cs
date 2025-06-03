using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.API.Data.Repositories
{
    public class TagRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public TagRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Models.Entities.Tag>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Tags.ToListAsync();
            }
        }

        public async Task<Models.Entities.Tag?> Find(int id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Tags.FindAsync(id);
            }
        }

        public async Task<Models.Entities.Tag> Create(Models.Entities.Tag tag)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Tags.Add(tag);
                await context.SaveChangesAsync();

                return tag;
            }
        }

        public async Task<Models.Entities.Tag> Update(Models.Entities.Tag tag)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Tags.Update(tag);
                await context.SaveChangesAsync();

                return tag;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Models.Entities.Tag tag = new Models.Entities.Tag()
                {
                    Id = id
                };

                context.Tags.Remove(tag);

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
