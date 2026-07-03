using MediatR;
using TaskFlow.Application.Features.Boards.Dtos;

namespace TaskFlow.Application.Features.Boards.Queries.CreateBoard
{
    public record CreateBoardCommand(string Title, Guid OwnerId = default) : IRequest<BoardDto>;
}
