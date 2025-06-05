using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public SupplierRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Supplier>> GetAll(CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Suppliers.ToListAsync(cancellationToken);
            }
        }

        public async Task<Supplier?> Find(Guid id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Suppliers.FindAsync(id, cancellationToken);
            }
        }

        public async Task<Supplier> Create(Supplier supplier, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Suppliers.Add(supplier);
                await context.SaveChangesAsync(cancellationToken);

                return supplier;
            }
        }

        public async Task<Supplier> Update(Supplier supplier, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Suppliers.Update(supplier);
                await context.SaveChangesAsync(cancellationToken);

                return supplier;
            }
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Supplier supplier = new Supplier()
                {
                    Id = id
                };

                context.Suppliers.Remove(supplier);

                return await context.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
