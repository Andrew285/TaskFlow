using MediatR;
using TaskFlow.Application.Features.Tasks.Dtos;

namespace TaskFlow.Application.Features.Tasks.Queries.GetTasksByBoard
{
    public record GetTasksByBoardQuery(Guid id) : IRequest<IEnumerable<TaskItemDto>>;
}
