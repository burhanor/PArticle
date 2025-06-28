using Domain.Contracts.Enums;
using Domain.Contracts.Interfaces;

namespace PArticle.Domain.Views
{
	public class VwArticleVote : IViewBase
	{
		public int ArticleId { get; set; }
		public ArticleVote Vote { get; set; }
		public int TotalVoteCount { get; set; }
	}
}
