using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlow.Application.Features.Boards.Queries.CreateBoard;
using TaskFlow.Application.Features.Boards.Queries.GetBoards;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoardsController(IMediator mediator): ControllerBase
    {
        private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")!);

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await mediator.Send(new GetBoardsQuery(CurrentUserId), ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateBoardCommand command,
            CancellationToken ct)
        {
            var commandWithOwner = command with { OwnerId = CurrentUserId };
            var result = await mediator.Send(commandWithOwner, ct);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
    }
}
