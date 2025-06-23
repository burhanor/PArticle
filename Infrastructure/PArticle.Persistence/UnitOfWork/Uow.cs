using PArticle.Application.Abstractions.Interfaces.Repositories;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Persistence.Context;
using PArticle.Persistence.Repositories.Concretes;

namespace PArticle.Persistence.UnitOfWork
{
	public class Uow(ArticleDbContext dbContext) : IUow
	{
		private readonly ArticleDbContext dbContext = dbContext;
		public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
		IReadRepository<T> IUow.GetReadRepository<T>() => new ReadRepository<T>(dbContext);
		IWriteRepository<T> IUow.GetWriteRepository<T>() => new WriteRepository<T>(dbContext);
		IViewRepository<T> IUow.GetViewRepository<T>() => new ViewRepository<T>(dbContext);
		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);
		public int SaveChanges() => dbContext.SaveChanges();

		#region Transaction
		public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => await dbContext.Database.BeginTransactionAsync(cancellationToken);
		public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
		{
			await SaveChangesAsync(cancellationToken);
			await dbContext.Database.CommitTransactionAsync(cancellationToken);
		}
		public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => await dbContext.Database.RollbackTransactionAsync(cancellationToken);

	
		#endregion



	}
}
