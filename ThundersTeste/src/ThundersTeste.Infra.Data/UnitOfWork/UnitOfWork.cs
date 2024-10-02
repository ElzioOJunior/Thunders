using System.Threading.Tasks;
using ThundersTeste.Domain.SeedWork;
using ThundersTeste.Infra.Data.Context.MongoDb;

namespace ThundersTeste.Infra.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _applicationDbContext;

	public UnitOfWork(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	public async Task<bool> CommitAsync()
	{
		return await _applicationDbContext.SaveEntitiesAsync();
	}
}
