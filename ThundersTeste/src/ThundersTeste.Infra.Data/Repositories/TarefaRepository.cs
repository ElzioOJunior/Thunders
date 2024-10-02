using System;
using System.Threading.Tasks;
using ThundersTeste.Infra.CrossCutting.Environments.Configurations;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;

namespace ThundersTeste.Infra.Data.Repositories;

public class TarefaRepository : RepositoryMongo<Tarefa>, ITarefaRepository
{
    public TarefaRepository(
        Context.MongoDb.ApplicationDbContext applicationDbContext,
        MongoDbConfiguration mongoDbConfiguration)
        : base(
            applicationDbContext,
            mongoDbConfiguration.Collections.Tarefa)
    { }

}
