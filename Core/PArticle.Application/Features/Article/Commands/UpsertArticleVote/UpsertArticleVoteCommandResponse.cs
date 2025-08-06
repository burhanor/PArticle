using Domain.Contracts.Enums;

namespace PArticle.Application.Features.Article.Commands.UpsertArticleVote
{
	public class UpsertArticleVoteCommandResponse
	{
		public ArticleVote Vote { get; set; }
		public int TotalVoteCount { get; set; }
	}
}
