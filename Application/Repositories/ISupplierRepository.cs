using Domain.Entities;

namespace Application.Repositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAll(CancellationToken cancellationToken);
        Task<Supplier?> Find(Guid id, CancellationToken cancellationToken);
        Task<Supplier> Create(Supplier supplier, CancellationToken cancellationToken);
        Task<Supplier> Update(Supplier supplier, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }
}
