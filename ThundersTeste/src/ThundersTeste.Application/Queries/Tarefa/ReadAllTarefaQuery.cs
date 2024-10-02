using System.Collections.Generic;
using ThundersTeste.Application.Dtos.Tarefa;

namespace ThundersTeste.Application.Queries.Tarefa;

public class ReadAllTarefaQuery : Query<ICollection<TarefaDto>>
{
    public ReadAllTarefaQuery()
    { }

}
