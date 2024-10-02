using ThundersTeste.Domain.Aggregates.TarefaAggregate;
using MongoDB.Bson.Serialization;

namespace ThundersTeste.Infra.Data.Mappings.MongoDb;

public class TarefaMap
{
    private readonly BsonClassMap<Tarefa> classMapper;

    public TarefaMap(BsonClassMap<Tarefa> classMapInit)
    {
        classMapper = classMapInit;
    }

    public void Configure()
    {
        classMapper.SetIgnoreExtraElements(true);
        classMapper.SetIgnoreExtraElementsIsInherited(true);

        classMapper.MapMember(entity => entity.Name).SetIsRequired(true);
        classMapper.MapMember(entity => entity.Description).SetIsRequired(true);

        classMapper.MapMember(entity => entity.CreatedAt).SetIgnoreIfNull(true);
        classMapper.MapMember(entity => entity.UpdatedAt).SetIgnoreIfNull(true);
    }
}
