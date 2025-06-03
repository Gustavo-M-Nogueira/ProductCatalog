using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Models.Entities;

namespace ProductCatalog.API.Data.Repositories
{
    public class ProductRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ProductRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Products.ToListAsync();
            }
        }

        public async Task<Product?> Find(Guid id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Products.FindAsync(id);
            }
        }

        public async Task<Product> Create(Product product)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();

                return product;
            }
        }

        public async Task<Product> Update(Product product)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();

                return product;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Product product = new Product()
                {
                    Id = id
                };

                context.Products.Remove(product);

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
