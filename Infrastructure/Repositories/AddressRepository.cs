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

        public async Task<IEnumerable<Address>> GetAll(CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Addresses.ToListAsync(cancellationToken);
            }
        }

        public async Task<Address?> Find(Guid id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Addresses.FindAsync(id, cancellationToken);
            }
        }

        public async Task<Address> Create(Address address, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Addresses.Add(address);
                await context.SaveChangesAsync(cancellationToken);

                return address;
            }
        }

        public async Task<Address> Update(Address address, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Addresses.Update(address);
                await context.SaveChangesAsync(cancellationToken);

                return address;
            }
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Address address = new Address()
                {
                    Id = id
                };

                context.Addresses.Remove(address);

                return await context.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
