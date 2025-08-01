using MediatR;
using System.Collections.Generic;

namespace PArticle.Application.Features.Stats.Queries.GetTopAuthors
{
	public class GetTopAuthorsQueryRequest(int limit):IRequest<IList<GetTopAuthorsQueryResponse>>
	{
		public int Limit { get; set; } = limit < 1 ? 1 : limit;

	}
}
