using System;

namespace ThundersTeste.Application.Dtos.Tarefa;

public class TarefaDto
{
    public TarefaDto(Domain.Aggregates.TarefaAggregate.Tarefa tarefa)
    {
        Id = tarefa.Id;
        Name = tarefa.Name;

        Description = tarefa.Description;

        CreatedAt = tarefa.CreatedAt;
        UpdatedAt = tarefa.UpdatedAt;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
