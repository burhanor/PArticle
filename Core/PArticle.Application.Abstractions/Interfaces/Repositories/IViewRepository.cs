using Domain.Contracts.Interfaces;
using System.Linq.Expressions;

namespace PArticle.Application.Abstractions.Interfaces.Repositories
{
	public interface IViewRepository<T> where T : class, IViewBase, new()
	{
		IQueryable<T> Query();
		Task<IList<T>> ToListAsync(CancellationToken cancellationToken);

		Task<IList<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken);
		Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate,  CancellationToken cancellationToken = default);

	}
}
