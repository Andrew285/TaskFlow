using MediatR;
using TaskFlow.Application.Features.Tasks.Dtos;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Features.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand(
       string Title,
       string? Description,
       TaskPriority Priority,
       DateTime? DueDate,
       Guid BoardId) : IRequest<TaskItemDto>;
}
