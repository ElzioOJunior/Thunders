using Moq;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;
using MediatR;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Application.Commands.Tarefa;
using ThundersTeste.Domain.SeedWork;
using ThundersTeste.Application.CommandHandlers.Tarefa;

namespace ThundersTeste.Tests.Unit.Tarefa
{

    public class UpdateTarefasCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_TarefasDto_When_Tarefas_Updated_Successfully()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateTarefaCommand(id, "nome teste","descricao teste");
            var mockRepository = new Mock<ITarefaRepository>();
            mockRepository.Setup(repo => repo.ReadById(It.IsAny<Guid>())).ReturnsAsync(new Domain.Aggregates.TarefaAggregate.Tarefa("nome teste","descricao teste"));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(true);
            var mockMediator = new Mock<IMediator>();
            var handler = new UpdateTarefaCommandHandler(mockUnitOfWork.Object, mockMediator.Object,  mockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TarefaDto>(result);
            Assert.Equal(command.Name, result.Name);
            mockRepository.Verify(repo => repo.ReadById(It.IsAny<Guid>()), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<Domain.Aggregates.TarefaAggregate.Tarefa>()), Times.Once);
            mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Returns_Null_When_Tarefas_Not_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new UpdateTarefaCommand(id, "nome teste", "descricao teste");
            var mockRepository = new Mock<ITarefaRepository>();
            mockRepository.Setup(repo => repo.ReadById(It.IsAny<Guid>())).ReturnsAsync((Domain.Aggregates.TarefaAggregate.Tarefa)null);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMediator = new Mock<IMediator>();
            var handler = new UpdateTarefaCommandHandler(mockUnitOfWork.Object, mockMediator.Object, mockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);

        }
    }
}
