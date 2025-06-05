using Domain.Entities;

namespace Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken);
        Task<Category?> Find(int id, CancellationToken cancellationToken);
        Task<Category> Create(Category category, CancellationToken cancellationToken);
        Task<Category> Update(Category category, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
