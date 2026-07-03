
namespace TaskFlow.Domain.Entities
{
    public class Board
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public User Owner { get; set; } = null!;
        public ICollection<TaskItem> Tasks { get; set; } = [];
    }
}
