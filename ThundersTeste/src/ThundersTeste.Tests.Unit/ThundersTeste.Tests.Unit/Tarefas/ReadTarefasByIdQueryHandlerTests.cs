using Moq;
using ThundersTeste.Application.Queries.Tarefa;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;
using MediatR;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Application.QueryHandlers.Tarefa;

namespace ThundersTeste.Tests.Unit.Tarefa
{

    public class ReadTarefaByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_TarefaDto_When_Valid_Id()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new ReadTarefaByIdQuery(id);
            var mockRepository = new Mock<ITarefaRepository>();
            var mockMediator = new Mock<IMediator>();

            var Tarefa = new Domain.Aggregates.TarefaAggregate.Tarefa("vaga 1", "descricao teste vaga 1");
            mockRepository.Setup(repo => repo.ReadById(It.IsAny<Guid>())).ReturnsAsync(Tarefa);

            var handler = new ReadTarefaByIdQueryHandler(mockMediator.Object, mockRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TarefaDto>(result);
            Assert.Equal(Tarefa.Id, result.Id);
            Assert.Equal(Tarefa.Name, result.Name);
        }

        [Fact]
        public async Task Handle_Returns_Default_When_Invalid_Id()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new ReadTarefaByIdQuery(id);
            var mockRepository = new Mock<ITarefaRepository>();
            var mockMediator = new Mock<IMediator>();

            mockRepository.Setup(repo => repo.ReadById(It.IsAny<Guid>())).ReturnsAsync((Domain.Aggregates.TarefaAggregate.Tarefa)null);

            var handler = new ReadTarefaByIdQueryHandler(mockMediator.Object, mockRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
