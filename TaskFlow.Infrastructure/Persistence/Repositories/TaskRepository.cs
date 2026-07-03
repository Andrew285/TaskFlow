using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Persistence.Repositories
{
    public class TaskRepository(AppDbContext db) : ITaskRepository
    {
        public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await db.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);

        public async Task<IEnumerable<TaskItem>> GetByBoardIdAsync(
            Guid boardId, CancellationToken ct = default)
            => await db.Tasks
                .Where(t => t.BoardId == boardId)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync(ct);

        public async Task AddAsync(TaskItem task, CancellationToken ct = default)
            => await db.Tasks.AddAsync(task, ct);

        public void Update(TaskItem task)
        {
            task.UpdatedAt = DateTime.UtcNow;
            db.Tasks.Update(task);
        }

        public void Delete(TaskItem task)
            => db.Tasks.Remove(task);
    }
}
