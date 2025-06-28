using Domain.Contracts.Enums;

namespace PArticle.Application.Features.Article.Queries.GetArticleVotes
{
	public class GetArticleVotesQueryResponse
	{
		public ArticleVote Vote { get; set; }
		public int TotalVoteCount { get; set; }
	}
}
