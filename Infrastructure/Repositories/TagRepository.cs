using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Tag = Domain.Entities.Tag;

namespace Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public TagRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Tag>> GetAll(CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Tags.ToListAsync(cancellationToken);
            }
        }

        public async Task<Tag?> Find(int id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Tags.FindAsync(id, cancellationToken);
            }
        }

        public async Task<Tag> Create(Tag tag, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Tags.Add(tag);
                await context.SaveChangesAsync(cancellationToken);

                return tag;
            }
        }

        public async Task<Tag> Update(Tag tag, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Tags.Update(tag);
                await context.SaveChangesAsync(cancellationToken);

                return tag;
            }
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Tag tag = new Tag()
                {
                    Id = id
                };

                context.Tags.Remove(tag);

                return await context.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
