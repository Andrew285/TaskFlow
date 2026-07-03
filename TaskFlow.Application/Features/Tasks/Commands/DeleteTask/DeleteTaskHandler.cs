using MediatR;
using TaskFlow.Application.Common.Interfaces;

namespace TaskFlow.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskHandler(IUnitOfWork uow)
        : IRequestHandler<DeleteTaskCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteTaskCommand request,
            CancellationToken cancellationToken)
        {
            var task = await uow.Tasks.GetByIdAsync(request.Id, cancellationToken);
            if (task is null)
                throw new KeyNotFoundException("Task is not found");

            uow.Tasks.Delete(task);
            await uow.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
