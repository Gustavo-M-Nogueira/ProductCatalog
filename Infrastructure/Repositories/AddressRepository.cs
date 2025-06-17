using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public AddressRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Address>> GetAll(CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Addresses.ToListAsync(cancellationToken);
            }
        }

        public async Task<Address?> Find(Guid id, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Addresses.FindAsync(id, cancellationToken);
            }
        }

        public async Task<Address> Create(Address address, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Addresses.Add(address);
                await context.SaveChangesAsync(cancellationToken);

                return address;
            }
        }

        public async Task<Address> Update(Address address, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Addresses.Update(address);
                await context.SaveChangesAsync(cancellationToken);

                return address;
            }
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                var address = await context.Addresses.FindAsync(new object[] { id }, cancellationToken);

                if (address is null)
                    return false;

                context.Addresses.Remove(address);

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
