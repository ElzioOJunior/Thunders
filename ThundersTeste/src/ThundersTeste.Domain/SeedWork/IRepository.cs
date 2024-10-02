using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ThundersTeste.Domain.SeedWork;

public interface IRepository<TEntity> where TEntity : IAggregateRoot
{
    void Add(TEntity obj);
    Task<ICollection<TEntity>> ReadAll(int limit = 100);
    Task DeleteById(Guid id);
    Task<TEntity> ReadById(Guid id);
    void Update(TEntity obj);
    void DropCollection();
    Task DropIndex();
}
