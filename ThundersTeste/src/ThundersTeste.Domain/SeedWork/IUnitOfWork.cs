using System.Threading.Tasks;

namespace ThundersTeste.Domain.SeedWork;

public interface IUnitOfWork
{
	Task<bool> CommitAsync();
}
