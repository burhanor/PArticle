using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Article.Queries.GetTopRatedArticles
{
	public class GetTopRatedArticlesQueryRequest(int count) : IRequest<IList<GetTopRatedArticlesQueryResponse>>
	{
		public int Count { get; set; } = count;
	}
}
