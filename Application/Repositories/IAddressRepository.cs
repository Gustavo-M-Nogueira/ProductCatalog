using Domain.Entities;

namespace Application.Repositories
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAll(CancellationToken cancellationToken);
        Task<Address?> Find(Guid id, CancellationToken cancellationToken);
        Task<Address> Create(Address address, CancellationToken cancellationToken);
        Task<Address> Update(Address address, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }
}
