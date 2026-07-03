
namespace TaskFlow.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        IBoardRepository Boards { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
