using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PArticle.Application.Abstractions.Interfaces;
using PArticle.Application.Abstractions.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PArticle.Persistence.Repositories.Concretes
{
	public class WriteRepository<T>(DbContext dbContext) : IWriteRepository<T> where T : class, IId, new()
	{
		private DbSet<T> Table { get => dbContext.Set<T>(); }
		public async Task AddAsync(T entity, CancellationToken cancellationToken) => await Table.AddAsync(entity, cancellationToken);
		public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken) => await Table.AddRangeAsync(entities, cancellationToken);

		public void Update(T entity) => Table.Update(entity);
		public void UpdateRange(IEnumerable<T> entities) => Table.UpdateRange(entities);

		public void Delete(T entity) => Table.Remove(entity);
		public void DeleteRange(IEnumerable<T> entities) => Table.RemoveRange(entities);
		public void Delete(Expression<Func<T, bool>> predicate) => Table.RemoveRange(Table.Where(predicate));

		public void Delete(List<int> ids) => Table.RemoveRange(Table.Where(x => ids.Contains(x.Id)));

	

	}
}
