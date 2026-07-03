using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Features.Tasks.Dtos
{
    public static class TaskItemMappingExtensions
    {
        public static TaskItemDto ToDto(this TaskItem task) =>
            new(
                task.Id,
                task.Title,
                task.Description,
                task.Status,
                task.Priority,
                task.DueDate,
                task.BoardId,
                task.CreatedAt);
    }
}
