using Domain.Entities;

namespace Application.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken);
        Task<Product?> Find(Guid id, CancellationToken cancellationToken);
        Task<Product> Create(Product product, CancellationToken cancellationToken);
        Task<Product> Update(Product product, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }
}
