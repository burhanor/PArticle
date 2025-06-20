using Domain.Contracts.Interfaces;
using System.Linq.Expressions;

namespace PArticle.Application.Abstractions.Interfaces.Repositories
{
	public interface IReadRepository<T> where T : class, IEntityBase, new()
	{
		IQueryable<T> Query();

		Task<IList<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken);

		Task<T?> FindAsync(int id, bool enableTracking = false, CancellationToken cancellationToken = default);
		Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool enableTracking = false,  CancellationToken cancellationToken = default);
		Task<TType?> GetAsync<TType>(Expression<Func<T, TType>> select, Expression<Func<T, bool>> predicate, bool enableTracking = false, CancellationToken cancellationToken = default);
		Task<IList<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, bool enableTracking = false,  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? currentPage = null, int? pageSize = null, CancellationToken cancellationToken = default);
		Task<IList<TType>> GetListAsync<TType>(Expression<Func<T, TType>> select, Expression<Func<T, bool>>? predicate = null, bool enableTracking = false,  Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? currentPage = null, int? pageSize = null, CancellationToken cancellationToken = default);
		Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
		Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
		Task<bool> UniqueAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

	}
}
