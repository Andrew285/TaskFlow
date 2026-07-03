using MediatR;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Features.Boards.Dtos;

namespace TaskFlow.Application.Features.Boards.Queries.GetBoards
{
    public class GetBoardsHandler(IUnitOfWork uow)
      : IRequestHandler<GetBoardsQuery, IEnumerable<BoardDto>>
    {
        public async Task<IEnumerable<BoardDto>> Handle(
            GetBoardsQuery request,
            CancellationToken cancellationToken)
        {
            var boards = await uow.Boards.GetByOwnerIdAsync(request.OwnerId, cancellationToken);
            return boards.Select(b => b.ToDto());
        }
    }
}
