using Domain.Entities;
using Tag = Domain.Entities.Tag;

namespace Application.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAll(CancellationToken cancellationToken);
        Task<Tag?> Find(int id, CancellationToken cancellationToken);
        Task<Tag> Create(Tag tag, CancellationToken cancellationToken);
        Task<Tag> Update(Tag tag, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
