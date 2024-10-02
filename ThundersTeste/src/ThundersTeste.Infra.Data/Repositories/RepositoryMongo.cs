using ThundersTeste.Domain.SeedWork;
using ThundersTeste.Infra.Data.Context.MongoDb;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThundersTeste.Infra.Data.Repositories;

public abstract class RepositoryMongo<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
{
    protected readonly ApplicationDbContext ApplicationDbContext;
    protected readonly IMongoCollection<TEntity> Collection;

    protected RepositoryMongo(ApplicationDbContext applicationDbContext, string collectionName)
    {
        ApplicationDbContext = applicationDbContext;
        Collection = ApplicationDbContext.GetCollection<TEntity>(collectionName);
    }

    public void Add(TEntity entity)
    {

        ApplicationDbContext.CreateOperation(
            async (IClientSessionHandle clientSessionHandle) =>
            {
                await Collection.InsertOneAsync(clientSessionHandle, entity);
                return entity;
            }
        );
    }

    public virtual async Task<ICollection<TEntity>> ReadAll(int limit = 100)
    {
        var filter = Builders<TEntity>.Filter.Empty;

        return await Collection.Find(filter).Limit(limit).ToListAsync();
    }

    public async Task DeleteById(Guid id)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);

        await Collection.DeleteOneAsync(filter);
    }

    public async Task<TEntity> ReadById(Guid id)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);

        return await Collection.Find(filter).SingleOrDefaultAsync();
    }

    public void Update(TEntity obj)
    {
        ApplicationDbContext.CreateOperation(
            async (IClientSessionHandle clientSessionHandle) =>
            {
                var filter = Builders<TEntity>.Filter.Eq(u => u.Id, obj.Id);

                await Collection.FindOneAndReplaceAsync(clientSessionHandle, filter, obj);
                return obj;
            }
        );
    }

    public void DropCollection()
    {
        Collection.Database.DropCollection(Collection.CollectionNamespace.CollectionName);
    }

    public async Task DropIndex()
    {
        try
        {
            var listOfIndex = Collection.Indexes.List().ToList();

            foreach (var index in listOfIndex)
            {
                var indexName = index.GetElement("name").Value.AsString;
                if (!indexName.Contains("_id_"))
                    await Collection.Indexes.DropOneAsync(indexName);
            }
        }
        catch (Exception) { }
    }
}
