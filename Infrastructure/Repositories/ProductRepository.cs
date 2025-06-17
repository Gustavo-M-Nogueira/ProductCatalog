using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ProductRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Products.ToListAsync(cancellationToken);
            }
        }

        public async Task<Product?> Find(Guid id, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Products.FindAsync(id, cancellationToken);
            }
        }

        public async Task<Product> Create(Product product, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Products.Add(product);
                await context.SaveChangesAsync(cancellationToken);

                return product;
            }
        }

        public async Task<Product> Update(Product product, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Products.Update(product);
                await context.SaveChangesAsync(cancellationToken);

                return product;
            }
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                var product = await context.Products.FindAsync(new object[] { id }, cancellationToken);

                if (product is null)
                    return false;

                context.Products.Remove(product);

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
