using FluentAssertions;
using NSubstitute;
using TaskFlow.Application.Common.Interfaces;
using TaskFlow.Application.Features.Boards.Queries.CreateBoard;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Tests.Features.Boards
{
    public class CreateBoardHandlerTests
    {
        private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
        private readonly IBoardRepository _boardRepo = Substitute.For<IBoardRepository>();
        private readonly CreateBoardHandler _handler;

        public CreateBoardHandlerTests()
        {
            uow.Boards.Returns(_boardRepo);
            _handler = new CreateBoardHandler(uow);
        }

        [Fact]
        public async Task Handle_ValidCommand_CallsAddAndSave()
        {
            // Arrange
            CreateBoardCommand command = new CreateBoardCommand("Test", Guid.NewGuid());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _boardRepo.Received(1).AddAsync(
                Arg.Is<Board>(b => b.Title == "Test"),
                Arg.Any<CancellationToken>());

            await uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_EmptyTitle_ShouldStillCreate()
        {
            var command = new CreateBoardCommand("", Guid.NewGuid());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Title.Should().BeEmpty();
        }
    }
}
