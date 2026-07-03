using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Features.Tasks.Commands.CreateTask;
using TaskFlow.Application.Features.Tasks.Commands.DeleteTask;
using TaskFlow.Application.Features.Tasks.Commands.UpdateTask;
using TaskFlow.Application.Features.Tasks.Queries.GetTasksByBoard;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController(IMediator mediator): ControllerBase
    {
        [HttpGet("board/{boardId}")]
        public async Task<IActionResult> GetByBoard(Guid boardId, CancellationToken ct)
        {
            var result = await mediator.Send(new GetTasksByBoardQuery(boardId), ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand command, CancellationToken ct)
        {
            var result = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetByBoard), new { boardId = result.BoardId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateTaskCommand command,
            CancellationToken ct)
        {
            if (id != command.Id)
                return BadRequest("ID in URL is different");

            var result = await mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await mediator.Send(new DeleteTaskCommand(id), ct);
            return NoContent();
        }
    }
}
