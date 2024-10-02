using Moq;
using ThundersTeste.Application.Queries.Tarefa;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;
using MediatR;
using ThundersTeste.Application.Dtos.Tarefa;
using ThundersTeste.Application.QueryHandlers.Tarefa;

namespace ThundersTeste.Tests.Unit.Tarefa
{

    public class ReadAllTarefaQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Collection_Of_TarefaDtos()
        {
            // Arrange
            var query = new ReadAllTarefaQuery();
            var mockRepository = new Mock<ITarefaRepository>();
            var mockMediator = new Mock<IMediator>();

            var tarefa = new List<Domain.Aggregates.TarefaAggregate.Tarefa>
        {
            new Domain.Aggregates.TarefaAggregate.Tarefa("vaga 1","vaga para testes 1"),
            new Domain.Aggregates.TarefaAggregate.Tarefa("vaga 2","vaga para testes 2")
        }; 
            mockRepository.Setup(repo => repo.ReadAll(100)).ReturnsAsync(tarefa);

            var handler = new ReadAllTarefaQueryHandler(mockMediator.Object, mockRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<TarefaDto>>(result);
            Assert.Equal(tarefa.Count, result.Count());
            foreach (var TarefaDto in result)
            {
                var matchingTarefa = tarefa.FirstOrDefault(c => c.Id == TarefaDto.Id);
                Assert.NotNull(matchingTarefa);
                Assert.Equal(matchingTarefa.Name, TarefaDto.Name);
            }
        }
    }
}
