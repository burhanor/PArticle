namespace PArticle.Subscribers.Core.Interfaces
{
	public interface IElasticSearchService<T> where T : class, IElasticEntity
	{
		Task UpsertAsync(string id, T entity);
		Task DeleteAsync(string id);
		Task<T?> GetByIdAsync(string id);

		Task<bool> DeleteByAsyncId(int id);
	
	}
}
