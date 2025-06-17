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

        public async Task<IEnumerable<Tag>> GetAll(CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Tags.ToListAsync(cancellationToken);
            }
        }

        public async Task<Tag?> Find(int id, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Tags.FindAsync(id, cancellationToken);
            }
        }

        public async Task<Tag> Create(Tag tag, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Tags.Add(tag);
                await context.SaveChangesAsync(cancellationToken);

                return tag;
            }
        }

        public async Task<Tag> Update(Tag tag, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Tags.Update(tag);
                await context.SaveChangesAsync(cancellationToken);

                return tag;
            }
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                //Tag tag = new Tag()
                //{
                //    Id = id
                //};


                var tag = await context.Tags.FindAsync(new object[] { id }, cancellationToken);

                if (tag is null)
                    return false;

                context.Tags.Remove(tag);

                try
                {
                    await context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                catch (Exception ex) 
                {
                    return false;
                }
            }
        }
    }
}
