using ThundersTeste.Domain.SeedWork;
using ThundersTeste.Infra.Data.Mappings.MongoDb;
using ThundersTeste.Infra.CrossCutting.Environments.Configurations;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MongoDB.Driver;
using MediatR;
using System;
using ThundersTeste.Domain.Aggregates.TarefaAggregate;

namespace ThundersTeste.Infra.Data.Context.MongoDb
{
    public class ApplicationDbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        private readonly IMediator _bus;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        private ConcurrentQueue<DatabaseOperation> _databaseOperations;

        public ApplicationDbContext(MongoDbConfiguration mongoDbConfiguration, IMediator mediator, ILogger<ApplicationDbContext> logger)
        {
            _logger = logger;
            _bus = mediator ?? throw new ArgumentNullException(nameof(mediator));

            var mongoSettings = MongoClientSettings.FromConnectionString(mongoDbConfiguration.ConnectionString);

            _mongoClient = new MongoClient(mongoSettings).WithWriteConcern(WriteConcern.W1);
            _mongoDatabase = _mongoClient.GetDatabase(mongoDbConfiguration.DatabaseName);

            _databaseOperations = new ConcurrentQueue<DatabaseOperation>();
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _mongoDatabase.GetCollection<T>(collectionName);
        }

        public static void ConfigureMappings()
        {
            BsonClassMap.RegisterClassMap<Tarefa>(c => new TarefaMap(c).Configure());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = new())
        {
            using (var clientSession = await _mongoClient.StartSessionAsync(null, cancellationToken))
            {

                try
                {
                    var alteredEntities = new List<Entity>();
                    while (_databaseOperations.TryDequeue(out var databaseOperation))
                        alteredEntities.Add(await databaseOperation.Operation(clientSession));

                    var allOperationsRan = _databaseOperations.Count == 0;
                    if (allOperationsRan)
                        _logger.LogDebug("Transacao comitada e todas as operacoes executadas com sucesso!");
                    else
                        _logger.LogWarning("Transacao comitada e operacoes executadas parcialmente!");

                    return allOperationsRan;
                }
                catch (Exception ex)
                {

                    _logger.LogError("Transacao abortada, erro ao salvar dados Ex: {0}", ex.ToString());
                    throw;
                }

            }
        }

        public void CreateOperation(Func<IClientSessionHandle, Task<Entity>> operation)
        {
            var newDatabaseOperation = new DatabaseOperation(operation);
            _databaseOperations?.Enqueue(newDatabaseOperation);
        }
    }

    internal class DatabaseOperation
    {
        public Func<IClientSessionHandle, Task<Entity>> Operation { get; }

        public DatabaseOperation(Func<IClientSessionHandle, Task<Entity>> operation)
        {
            Operation = operation;
        }
    }

}
