using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<TaskItem>> GetByBoardIdAsync(Guid boardId, CancellationToken ct);
        Task AddAsync(TaskItem task, CancellationToken ct = default);
        void Update(TaskItem task);
        void Delete(TaskItem task);
    }
}
