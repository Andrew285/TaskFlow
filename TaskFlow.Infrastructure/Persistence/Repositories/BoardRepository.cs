using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Persistence.Repositories
{
    public class BoardRepository(AppDbContext db) : IBoardRepository
    {
        public async Task<Board?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await db.Boards
                .Include(b => b.Tasks)
                .FirstOrDefaultAsync(b => b.Id == id, ct);

        public async Task<IEnumerable<Board>> GetByOwnerIdAsync(
            Guid ownerId, CancellationToken ct = default)
            => await db.Boards
                .Include(b => b.Tasks)
                .Where(b => b.OwnerId == ownerId)
                .ToListAsync(ct);

        public async Task AddAsync(Board board, CancellationToken ct = default)
            => await db.Boards.AddAsync(board, ct);

        public void Delete(Board board)
            => db.Boards.Remove(board);
    }
}
