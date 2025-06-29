using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Abstractions.Interfaces.ElasticSearch
{
	public interface IElasticSearchService<T> where T :class
	{
		Task<(List<T> Results, int TotalCount)> SearchAsync(string? keyword, int? page = null, int? pageSize = null);
	}
}
