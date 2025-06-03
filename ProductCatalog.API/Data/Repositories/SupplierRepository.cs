using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Models.Entities;

namespace ProductCatalog.API.Data.Repositories
{
    public class SupplierRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public SupplierRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Suppliers.ToListAsync();
            }
        }

        public async Task<Supplier?> Find(Guid id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Suppliers.FindAsync(id);
            }
        }

        public async Task<Supplier> Create(Supplier supplier)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Suppliers.Add(supplier);
                await context.SaveChangesAsync();

                return supplier;
            }
        }

        public async Task<Supplier> Update(Supplier supplier)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Suppliers.Update(supplier);
                await context.SaveChangesAsync();

                return supplier;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Supplier supplier = new Supplier()
                {
                    Id = id
                };

                context.Suppliers.Remove(supplier);

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
