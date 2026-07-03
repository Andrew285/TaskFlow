using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Board>> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default);
        Task AddAsync(Board board, CancellationToken ct = default);
        void Delete(Board board);
    }
}
