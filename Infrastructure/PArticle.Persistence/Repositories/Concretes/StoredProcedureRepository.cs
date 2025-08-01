using Microsoft.EntityFrameworkCore;
using PArticle.Application.Abstractions.Interfaces.Repositories;
using PArticle.Shared.Models;

namespace PArticle.Persistence.Repositories.Concretes
{
	public class StoredProcedureRepository(DbContext dbContext) : IStoredProcedureRepository
	{

		private readonly DbContext dbContext = dbContext;

		public Task<List<T>> ExecuteProcedureAsync<T>(string sql, CancellationToken cancellationToken, params object[] parameters) where T : class, new()
		{
			return dbContext.Set<T>().FromSqlRaw(sql, parameters).ToListAsync(cancellationToken);
		}
		public Task<List<T>> ExecuteProcedureAsync<T>(string sql, CancellationToken cancellationToken) where T : class, new()
		{
			return dbContext.Set<T>().FromSqlRaw(sql).ToListAsync(cancellationToken);
		}

		public async Task<IList<GetTopArticle>> GetTopArticles(DateTime startDate, DateTime endDate, int count, CancellationToken cancellationToken)
		{
			return await dbContext.Set<GetTopArticle>().FromSqlRaw("EXEC GetTopArticles @StartDate={0},@EndDate={1},@N={2}", startDate,endDate, count).ToListAsync(cancellationToken);
		}
		public async Task<IList<GetArticleRate>> GetArticleRates(DateTime startDate, DateTime endDate, int count, CancellationToken cancellationToken)
		{
			return await dbContext.Set<GetArticleRate>().FromSqlRaw("EXEC GetArticleRates @StartDate={0},@EndDate={1},@N={2}", startDate, endDate, count).ToListAsync(cancellationToken);
		}
		public async Task<IList<GetTopAuthor>> GetTopAuthors(int count, CancellationToken cancellationToken)
		{
			return await dbContext.Set<GetTopAuthor>().FromSqlRaw("EXEC GetTopAuthors @N={0}", count).ToListAsync(cancellationToken);
		}

		public async Task<IList<GetTopCategory>> GetTopCategories(int count, CancellationToken cancellationToken)
		{
			return await dbContext.Set<GetTopCategory>().FromSqlRaw("EXEC GetTopCategories @N={0}", count).ToListAsync(cancellationToken);
		}

		public async Task<IList<GetTopTag>> GetTopTags(int count, CancellationToken cancellationToken)
		{
			return await dbContext.Set<GetTopTag>().FromSqlRaw("EXEC GetTopTags @N={0}", count).ToListAsync(cancellationToken);
		}



	}
}
