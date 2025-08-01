using Domain.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using PArticle.Application.Abstractions.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Persistence.Repositories.Concretes
{
	public class ViewRepository<T>(DbContext dbContext) : IViewRepository<T> where T : class, IViewBase, new()
	{
		private readonly DbContext dbContext = dbContext;
		private DbSet<T> Table { get => dbContext.Set<T>(); }
		public IQueryable<T> Query()
		{
			return Table.AsNoTracking();
		}

		public async Task<IList<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken)
		{
			return await query.ToListAsync(cancellationToken);
		}

		public Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
		{
			return ToListAsync(Table.Where(predicate), cancellationToken);
		}

		public async  Task<IList<T>> ToListAsync(CancellationToken cancellationToken)
		{
			return await Table.ToListAsync(cancellationToken);
		}
	}
}
