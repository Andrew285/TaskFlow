using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {
        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await db.Users.AddAsync(user, ct);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        }
    }
}
