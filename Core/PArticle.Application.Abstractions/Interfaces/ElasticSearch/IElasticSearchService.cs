namespace PArticle.Application.Abstractions.Interfaces.ElasticSearch
{
	public interface IElasticSearchService<T> where T :class
	{
		Task<(List<T> Results, int TotalCount)> SearchAsync(string? keyword, int? page = null, int? pageSize = null);
		Task<(List<T> Results, int TotalCount)> SearchAsync(string status,string? keyword, int? page = null, int? pageSize = null);
		Task<(List<T> Results, int TotalCount)> SearchByCategoryAsync(string? slug, int? page = null, int? pageSize = null);
	}
}
