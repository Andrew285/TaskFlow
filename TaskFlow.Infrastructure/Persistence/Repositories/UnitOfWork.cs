using TaskFlow.Application.Common.Interfaces;

namespace TaskFlow.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork(
        AppDbContext db,
        ITaskRepository tasks,
        IBoardRepository boards,
        IUserRepository users) : IUnitOfWork
    {
        public ITaskRepository Tasks => tasks;
        public IBoardRepository Boards => boards;
        public IUserRepository Users => users;

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await db.SaveChangesAsync(ct);
    }
}
