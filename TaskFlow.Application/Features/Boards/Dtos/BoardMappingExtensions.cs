using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Features.Boards.Dtos
{
    public static class BoardMappingExtensions
    {
        public static BoardDto ToDto(this Board board) =>
            new(
                board.Id,
                board.Title,
                board.OwnerId,
                board.CreatedAt,
                board.Tasks?.Count ?? 0);
    }
}
