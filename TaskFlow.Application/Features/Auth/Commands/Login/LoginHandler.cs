using MediatR;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Features.Auth.Commands.Register;

namespace TaskFlow.Application.Features.Auth.Commands.Login
{
    public class LoginHandler(
        IUnitOfWork uow,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtGenerator) : IRequestHandler<LoginCommand, AuthResult>
    {
        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await uow.Users.GetByEmailAsync(request.Email, ct);
            if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Incorrect email or password");

            var token = jwtGenerator.GenerateToken(user);

            return new AuthResult(token, user.Id, user.Email, user.Name);
        }
    }
}
