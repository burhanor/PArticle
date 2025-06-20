using Domain.Contracts.Interfaces;
using System.Linq.Expressions;

namespace PArticle.Application.Abstractions.Interfaces.Repositories
{
	public interface IWriteRepository<T> where T : class, IEntityBase, new()
	{
		public Task AddAsync(T entity, CancellationToken cancellationToken);
		public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
		public void Update(T entity);
		public void UpdateRange(IEnumerable<T> entities);
		public void Delete(T entity);
		public void DeleteRange(IEnumerable<T> entities);
		public void Delete(Expression<Func<T, bool>> predicate);
		public void Delete(List<int> ids);

	}
}
