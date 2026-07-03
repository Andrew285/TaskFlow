using MediatR;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator
        ) : IRequestHandler<RegisterCommand, AuthResult>
    {
        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User? existing = await unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);
            if (existing is not null)
            {
                throw new InvalidOperationException("Email is already registered");
            }

            User user = new User
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = passwordHasher.Hash(request.Password)
            };

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            string token = jwtTokenGenerator.GenerateToken(user);

            return new AuthResult
            (
                token,
                user.Id,
                user.Email,
                user.Name
            );
        }
    }
}
