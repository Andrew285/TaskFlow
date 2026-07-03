using MediatR;
using TaskFlow.Application.Features.Auth.Commands.Register;

namespace TaskFlow.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResult>;
}
