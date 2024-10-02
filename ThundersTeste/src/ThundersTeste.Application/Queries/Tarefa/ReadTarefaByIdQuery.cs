using System;
using ThundersTeste.Application.Dtos.Tarefa;

namespace ThundersTeste.Application.Queries.Tarefa;

public class ReadTarefaByIdQuery : Query<TarefaDto>
{
    public ReadTarefaByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set;}
}
