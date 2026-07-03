using FluentAssertions;
using NSubstitute;
using System.Reflection.Metadata;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Features.Auth.Commands.Register;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Tests.Features.Auth
{
    public class RegisterHandleTests
    {
        private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();
        private readonly IPasswordHasher passwordHasher = Substitute.For<IPasswordHasher>();
        private readonly IJwtTokenGenerator jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        private readonly RegisterHandler handler;

        public RegisterHandleTests()
        {
            unitOfWork.Users.Returns(userRepository);
            handler = new RegisterHandler(unitOfWork, passwordHasher, jwtTokenGenerator);
        }

        [Fact]
        public async Task Handle_NewEmail_ReturnsAuthResult()
        {
            // Arrange
            userRepository.GetByEmailAsync("new@test.com", CancellationToken.None).Returns((User?)null);
            passwordHasher.Hash("Password123!").Returns("hashed_password");
            jwtTokenGenerator.GenerateToken(Arg.Any<User>()).Returns("jwt_token");

            RegisterCommand command = new RegisterCommand("new@test.com", "Password123!", "John");

            // Act
            AuthResult result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Email.Should().Be("new@test.com");
            result.Name.Should().Be("John");
            result.Token.Should().Be("jwt_token");
        }

        [Fact]
        public async Task Handle_ExistingEmail_ThrowsInvalidOperationException()
        {
            // Arrange
            userRepository.GetByEmailAsync("exists@test.com", Arg.Any<CancellationToken>())
                .Returns(new User { Email = "exists@test.com" });

            RegisterCommand command = new RegisterCommand("exists@test.com", "Password123!", "Іван");

            // Act
            var result = () => handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Email is already registered");
        }

        [Fact]
        public async Task Handle_NewUser_PasswordIsHashed()
        {
            // Arrange
            userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns((User?)null);
            passwordHasher.Hash("Password123!").Returns("$2a$hashed");
            jwtTokenGenerator.GenerateToken(Arg.Any<User>()).Returns("token");

            RegisterCommand command = new RegisterCommand("test@test.com", "Password123!", "John");

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            passwordHasher.Received(1).Hash("Password123!");
            await userRepository.Received(1).AddAsync(
                Arg.Is<User>(u => u.PasswordHash == "$2a$hashed"),
                Arg.Any<CancellationToken>());
        }
    }
}
