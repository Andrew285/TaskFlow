using MediatR;

namespace TaskFlow.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(string Email, string Password, string Name) : IRequest<AuthResult>;

    public record AuthResult(string Token, Guid UserId, string Email, string Name);
}
