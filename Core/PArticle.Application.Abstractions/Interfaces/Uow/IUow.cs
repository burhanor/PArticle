using PArticle.Application.Abstractions.Interfaces.Repositories;

namespace PArticle.Application.Abstractions.Interfaces.Uow
{
	public interface IUow
	{
		IReadRepository<T> GetReadRepository<T>() where T : class, IId, new();
		IWriteRepository<T> GetWriteRepository<T>() where T : class, IId, new();
		Task BeginTransactionAsync(CancellationToken cancellationToken = default);
		Task CommitTransactionAsync(CancellationToken cancellationToken = default);
		Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		int SaveChanges();
	}
}
