using Domain.Contracts.Enums;
using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Article.Commands.ResetArticleVotes
{
	public class ResetArticleVotesCommandRequest(int articleId):IRequest<ResponseContainer<Unit>>	
	{
		public ResetArticleVotesCommandRequest(int articleId, ArticleVote? voteType) : this(articleId)
		{
			VoteType = voteType;
		}

		public int ArticleId { get; set; } = articleId;
		public ArticleVote? VoteType { get; set; }

		
	}
}
