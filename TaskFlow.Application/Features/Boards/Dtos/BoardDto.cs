
namespace TaskFlow.Application.Features.Boards.Dtos
{
    public record BoardDto(
        Guid Id,
        string Title,
        Guid OwnerId,
        DateTime CreatedAt,
        int TasksCount);
}
