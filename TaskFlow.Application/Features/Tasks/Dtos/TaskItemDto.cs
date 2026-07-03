using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Features.Tasks.Dtos
{
    public record TaskItemDto(
        Guid Id,
        string Title,
        string? Description,
        Domain.Enums.TaskStatus Status,
        TaskPriority Priority,
        DateTime? DueDate,
        Guid BoardId,
        DateTime CreatedAt);
}
