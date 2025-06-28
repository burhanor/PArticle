using Domain.Contracts.Enums;
using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.UpsertArticleVote
{
	public class UpsertArticleVoteCommandRequest(int articleId, ArticleVote vote):IRequest<ResponseContainer<Unit>>
	{
		public int ArticleId { get; set; } = articleId;
		public ArticleVote Vote { get; set; } = vote;
		public string IpAddress { get; set; } = string.Empty;
		public DateTime VoteDate { get; set; }=DateTime.Now;
	}
}
