using ThundersTeste.Domain.SeedWork;
using ThundersTeste.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThundersTeste.Infra.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    protected readonly ApplicationDbContext ApplicationDbContext;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
        DbSet = applicationDbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        ApplicationDbContext.Add(entity);
    }

    public Task DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void DropCollection()
    {
        throw new NotImplementedException();
    }

    public Task DropIndex()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<TEntity>> ReadAll(int limit = 100)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> ReadById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity obj)
    {
        throw new NotImplementedException();
    }
}
