using Moq;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;
using MediatR;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Application.Commands.Tarefa;
using ThundersTeste.Application.CommandHandlers.Tarefa;
using ThundersTeste.Domain.SeedWork;

namespace ThundersTeste.Tests.Unit.Tarefa
{
    public class CreateTarefaCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_TarefaDto_When_Created_Successfully()
        {
            // Arrange
            var command = new CreateTarefaCommand { Name = "Junior teste" };
            var mockRepository = new Mock<ITarefaRepository>();
            mockRepository.Setup(repo => repo.Add(It.IsAny<Domain.Aggregates.TarefaAggregate.Tarefa>()));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(true);
            var mockMediator = new Mock<IMediator>();
            var handler = new CreateTarefaCommandHandler(mockUnitOfWork.Object, mockMediator.Object, mockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TarefaDto>(result);
            Assert.Equal(command.Name, result.Name);
            mockRepository.Verify(repo => repo.Add(It.IsAny<Domain.Aggregates.TarefaAggregate.Tarefa>()), Times.Once);
            mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Returns_Exception_Notification_When_Domain_Exception_Occurs()
        {
            // Arrange
            var command = new CreateTarefaCommand { Name = "Invalid Name" };
            var mockRepository = new Mock<ITarefaRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMediator = new Mock<IMediator>();
            var handler = new CreateTarefaCommandHandler(mockUnitOfWork.Object, mockMediator.Object, mockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

    }
}
