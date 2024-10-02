using ThundersTeste.Domain.SeedWork;
using System;

namespace ThundersTeste.Domain.Aggregates.TarefaAggregate;

public class Tarefa : Entity, IAggregateRoot
{
    public Tarefa(string name, string description)
    {
        ValidateField<Tarefa, string>(e => e.Name, string.IsNullOrEmpty(name));

        SetId();

        Name = name;
        Description = description;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.Now;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}
