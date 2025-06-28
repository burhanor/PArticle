using Domain.Contracts.Enums;
using MediatR;

namespace PArticle.Application.Features.Article.Queries.GetArticleVotes
{
	public class GetArticleVotesQueryRequest(int articleId):IRequest<IList<GetArticleVotesQueryResponse>>
	{
		public GetArticleVotesQueryRequest(int articleId, ArticleVote? vote) : this(articleId)
		{
			Vote = vote;
		}

		public int ArticleId { get; set; } = articleId;
		public ArticleVote? Vote { get; set; }
	}
}
