using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Models.Entities;

namespace ProductCatalog.API.Data.Repositories
{
    public class AddressRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public AddressRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Addresses.ToListAsync();
            }
        }

        public async Task<Address?> Find(Guid id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Addresses.FindAsync(id);
            }
        }

        public async Task<Address> Create(Address address)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Addresses.Add(address);
                await context.SaveChangesAsync();

                return address;
            }
        }

        public async Task<Address> Update(Address address)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                context.Addresses.Update(address);
                await context.SaveChangesAsync();

                return address;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (ApplicationDbContext context = _contextFactory.CreateDbContext())
            {
                Address address = new Address()
                {
                    Id = id
                };

                context.Addresses.Remove(address);

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
