using MediatR;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Features.Tasks.Dtos;

namespace TaskFlow.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskHandler(IUnitOfWork uow)
        : IRequestHandler<UpdateTaskCommand, TaskItemDto>
    {
        public async Task<TaskItemDto> Handle(
            UpdateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var task = await uow.Tasks.GetByIdAsync(request.Id, cancellationToken);
            if (task is null)
                throw new KeyNotFoundException("Task is not found");

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.Priority = request.Priority;
            task.DueDate = request.DueDate;

            uow.Tasks.Update(task);
            await uow.SaveChangesAsync(cancellationToken);

            return task.ToDto();
        }
    }
}
