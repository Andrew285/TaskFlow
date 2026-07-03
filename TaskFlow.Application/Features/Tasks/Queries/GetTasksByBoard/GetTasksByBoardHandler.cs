using MediatR;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Features.Tasks.Dtos;

namespace TaskFlow.Application.Features.Tasks.Queries.GetTasksByBoard
{
    public class GetTasksByBoardHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTasksByBoardQuery, IEnumerable<TaskItemDto>>
    {
        public async Task<IEnumerable<TaskItemDto>> Handle(GetTasksByBoardQuery request, CancellationToken cancellationToken)
        {
            var tasks = await unitOfWork.Tasks.GetByBoardIdAsync(request.id, cancellationToken);
            return tasks.Select(t => t.ToDto());
        }
    }
}
