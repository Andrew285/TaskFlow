using MediatR;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Features.Boards.Dtos;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Features.Boards.Queries.CreateBoard
{
    public class CreateBoardHandler(IUnitOfWork uow)
       : IRequestHandler<CreateBoardCommand, BoardDto>
    {
        public async Task<BoardDto> Handle(
            CreateBoardCommand request,
            CancellationToken cancellationToken)
        {
            var board = new Board
            {
                Title = request.Title,
                OwnerId = request.OwnerId,
            };

            await uow.Boards.AddAsync(board, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return board.ToDto();
        }
    }
}
