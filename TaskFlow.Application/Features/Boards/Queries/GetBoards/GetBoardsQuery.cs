using MediatR;
using TaskFlow.Application.Features.Boards.Dtos;

namespace TaskFlow.Application.Features.Boards.Queries.GetBoards
{
    public record GetBoardsQuery(Guid OwnerId) : IRequest<IEnumerable<BoardDto>>;
}
