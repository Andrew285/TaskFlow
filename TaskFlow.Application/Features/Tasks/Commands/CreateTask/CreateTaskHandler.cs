using MediatR;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Features.Tasks.Dtos;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskHandler(IUnitOfWork uow)
        : IRequestHandler<CreateTaskCommand, TaskItemDto>
    {
        public async Task<TaskItemDto> Handle(
            CreateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var board = await uow.Boards.GetByIdAsync(request.BoardId, cancellationToken);
            if (board is null)
                throw new KeyNotFoundException("Board is not found");

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                DueDate = request.DueDate,
                BoardId = request.BoardId
            };

            await uow.Tasks.AddAsync(task, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return task.ToDto();
        }
    }
}
