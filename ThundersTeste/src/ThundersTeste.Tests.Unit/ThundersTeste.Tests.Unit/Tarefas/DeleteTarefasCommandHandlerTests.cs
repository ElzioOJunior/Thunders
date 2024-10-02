using Moq;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;
using MediatR;
using ThundersTeste.Application.Commands.Tarefa;
using ThundersTeste.Application.CommandHandlers.Tarefa;
using ThundersTeste.Domain.SeedWork;

namespace ThundersTeste.Tests.Unit.Tarefas
{

    public class DeleteTarefasCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Unit_When_Tarefas_Deleted_Successfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteTarefaCommand(id);
            var mockRepository = new Mock<ITarefaRepository>();
            mockRepository.Setup(repo => repo.ReadById(It.IsAny<Guid>())).ReturnsAsync(new Domain.Aggregates.TarefaAggregate.Tarefa("teste", "descrição teste"));
            mockRepository.Setup(repo => repo.DeleteById(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(true);
            var mockMediator = new Mock<IMediator>();
            var handler = new DeleteTarefaCommandHandler(mockUnitOfWork.Object, mockMediator.Object, mockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepository.Verify(repo => repo.ReadById(It.IsAny<Guid>()), Times.Once);
            mockRepository.Verify(repo => repo.DeleteById(It.IsAny<Guid>()), Times.Once);
            mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Returns_Exception_Notification_When_Tarefas_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new DeleteTarefaCommand(id);
            var mockRepository = new Mock<ITarefaRepository>();
            mockRepository.Setup(repo => repo.ReadById(It.IsAny<Guid>())).ReturnsAsync((Domain.Aggregates.TarefaAggregate.Tarefa)null);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMediator = new Mock<IMediator>();
            var handler = new DeleteTarefaCommandHandler(mockUnitOfWork.Object, mockMediator.Object, mockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

        }
      
    }
}
