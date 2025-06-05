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

        public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Products.ToListAsync(cancellationToken);
            }
        }

        public async Task<Product?> Find(Guid id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Products.FindAsync(id, cancellationToken);
            }
        }

        public async Task<Product> Create(Product product, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Products.Add(product);
                await context.SaveChangesAsync(cancellationToken);

                return product;
            }
        }

        public async Task<Product> Update(Product product, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Products.Update(product);
                await context.SaveChangesAsync(cancellationToken);

                return product;
            }
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Product product = new Product()
                {
                    Id = id
                };

                context.Products.Remove(product);

                return await context.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
