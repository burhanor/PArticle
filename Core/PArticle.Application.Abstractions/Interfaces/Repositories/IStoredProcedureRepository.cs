using PArticle.Shared.Models;

namespace PArticle.Application.Abstractions.Interfaces.Repositories
{
	public interface IStoredProcedureRepository
	{
		Task<List<T>> ExecuteProcedureAsync<T>(string sql, CancellationToken cancellationToken, params object[] parameters) where T : class, new();
		public Task<List<T>> ExecuteProcedureAsync<T>(string sql,CancellationToken cancellationToken) where T : class, new();

		public Task<IList<GetTopTag>> GetTopTags(int count, CancellationToken cancellationToken);
		public Task<IList<GetTopCategory>> GetTopCategories(int count, CancellationToken cancellationToken);
		public Task<IList<GetTopAuthor>> GetTopAuthors(int count, CancellationToken cancellationToken);
		public Task<IList<GetTopArticle>> GetTopArticles(DateTime startDate, DateTime endDate, int count, CancellationToken cancellationToken);
		public Task<IList<GetArticleRate>> GetArticleRates(DateTime startDate, DateTime endDate, int count, CancellationToken cancellationToken);
	}
}
