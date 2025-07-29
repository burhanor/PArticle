using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;

namespace PArticle.Subscribers.Core.Concretes
{
	public class ElasticSearchService<T> : IElasticSearchService<T> where T : class, IElasticEntity
	{
		private readonly ElasticsearchClient _client;
		private readonly string _indexName;
		private readonly ElasticSearchModel esm;
		public ElasticSearchService(IOptions<ElasticSearchModel> options)
		{
			esm = options.Value;
			_indexName = esm.IndexName.ToLower();

			var settings = new ElasticsearchClientSettings(new Uri(esm.Uri))
				.DefaultIndex(_indexName);

			_client = new ElasticsearchClient(settings);

			// İndeks varsa oluşturma
			if (!_client.Indices.Exists(_indexName).Exists)
			{
				_client.Indices.Create(_indexName); 
			}
		}

		
		public async Task UpsertAsync(string id, T entity)
		{
			var request = new IndexRequest<T>(entity, _indexName)
			{
				Id = id,
			};

			var response = await _client.IndexAsync(request);

			if (!response.IsValidResponse)
			{
				throw new Exception($"Upsert işlemi başarısız: {response.DebugInformation}");
			}
		}


		public async Task DeleteAsync(string id)
		{
			await _client.DeleteAsync<T>(id, d => d.Index(_indexName));
		}

		public async Task<T?> GetByIdAsync(string id)
		{
			var response = await _client.GetAsync<T>(id, g => g.Index(_indexName));
			return response.Found ? response.Source : null;
		}

		public async Task<bool> DeleteByAsyncId(int id)
		{
			var response = await _client.DeleteByQueryAsync<T>(_indexName, q => q
			.Query(q => q.Term(new TermQuery
			   {
				   Field = "id",
				   Value = id
			   })));

			if (!response.IsValidResponse)
			{
				return false;
			}
			return true;

		}
	}

}
