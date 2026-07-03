using MediatR;
using TaskFlow.Application.Features.Tasks.Dtos;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Features.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand(
        Guid Id,
        string Title,
        string? Description,
        Domain.Enums.TaskStatus Status,
        TaskPriority Priority,
        DateTime? DueDate) : IRequest<TaskItemDto>;
}
