using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using PArticle.Application.Abstractions.Interfaces.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace PArticle.Infrastructure.ElasticSearch
{
	public class ElasticSearchService<T>: IElasticSearchService<T> where T : class
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
		Fuzziness fuzziness = new Fuzziness("AUTO");

		public async Task<(List<T> Results, int TotalCount)> SearchAsync(string status, string? keyword, int? page = null, int? pageSize = null)
		{
			int from = 0;
			int size = 10000; 

			if (page.HasValue && pageSize.HasValue)
			{
				from = (page.Value - 1) * pageSize.Value;
				size = pageSize.Value;
			}

			var response = await _client.SearchAsync<T>(s =>
			{
				s = s.Indices(_indexName).From(from).Size(size);
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					s = s.Query(q => q
						.Bool(b => b
							.Should(
								sq => sq.Match(m => m.Field("title").Query(keyword).Fuzziness(fuzziness)),
								sq => sq.Match(m => m.Field("content").Query(keyword).Fuzziness(fuzziness)),
								sq => sq.Match(m => m.Field("categories.name").Query(keyword).Fuzziness(fuzziness)),
								sq => sq.Match(m => m.Field("tags.name").Query(keyword).Fuzziness(fuzziness))
							)
							.MinimumShouldMatch(1)
							.Filter(f => f.Term(t => t.Field("status.keyword").Value(status.ToString())) 
							)
						)
					);
				}
			});

			if (!response.IsValidResponse)
			{
				throw new Exception($"Elasticsearch search error: {response.DebugInformation}");
			}

			return (response.Documents.ToList(), (int)response.Total);
		}

		public async Task<(List<T> Results, int TotalCount)> SearchAsync(string? keyword,  int? page = null, int? pageSize = null)
		{
			int from = 0;
			int size = 10000;

			if (page.HasValue && pageSize.HasValue)
			{
				from = (page.Value - 1) * pageSize.Value;
				size = pageSize.Value;
			}

			var response = await _client.SearchAsync<T>(s =>
			{
				s = s.Indices(_indexName).From(from).Size(size);
				if (!string.IsNullOrWhiteSpace(keyword))
				{
					s = s.Query(q => q
						.Bool(b => b
							.Should(
								sq => sq.Match(m => m.Field("title").Query(keyword).Fuzziness(fuzziness)),
								sq => sq.Match(m => m.Field("content").Query(keyword).Fuzziness(fuzziness)),
								sq => sq.Match(m => m.Field("categories.name").Query(keyword).Fuzziness(fuzziness)),
								sq => sq.Match(m => m.Field("tags.name").Query(keyword).Fuzziness(fuzziness))
							)
							.MinimumShouldMatch(1)
						)
					);
				}
			});

			if (!response.IsValidResponse)
			{
				throw new Exception($"Elasticsearch search error: {response.DebugInformation}");
			}

			return (response.Documents.ToList(), (int)response.Total);
		}



		public async Task<(List<T> Results, int TotalCount)> SearchByCategoryAsync(string? slug, int? page = null, int? pageSize = null)
		{

			int from = 0;
			int size = 10000;

			if (page.HasValue && pageSize.HasValue)
			{
				from = (page.Value - 1) * pageSize.Value;
				size = pageSize.Value;
			}

			var response = await _client.SearchAsync<T>(s =>
			{
				s = s.Indices(_indexName).From(from).Size(size);
				if (!string.IsNullOrWhiteSpace(slug))
				{
					s = s.Query(q => q
								.Bool(b => b
									.Must(m => m.Term(t => t.Field("status.keyword").Value("Published")),
									m => m.Term(t => t.Field("categories.slug.keyword").Value(slug))
									)
								)
							);
				}
			});

			if (!response.IsValidResponse)
			{
				throw new Exception($"Elasticsearch search error: {response.DebugInformation}");
			}

			return (response.Documents.ToList(), (int)response.Total);

		}


		public async Task<(List<T> Results, int TotalCount)> SearchByAuthorAsync(int authorId, int? page = null, int? pageSize = null)
		{

			int from = 0;
			int size = 10000;

			if (page.HasValue && pageSize.HasValue)
			{
				from = (page.Value - 1) * pageSize.Value;
				size = pageSize.Value;
			}

			var response = await _client.SearchAsync<T>(s =>
			{
				s = s.Indices(_indexName).From(from).Size(size);
				s = s.Query(q => q
								.Bool(b => b
									.Must(m => m.Term(t => t.Field("status.keyword").Value("Published")),
									m => m.Term(t => t.Field("userId").Value(authorId))
									)
								)
							);
			});

			if (!response.IsValidResponse)
			{
				throw new Exception($"Elasticsearch search error: {response.DebugInformation}");
			}

			return (response.Documents.ToList(), (int)response.Total);

		}

	}
}
