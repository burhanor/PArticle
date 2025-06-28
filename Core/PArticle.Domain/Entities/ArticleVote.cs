using Domain.Contracts.Enums;
using PArticle.Domain.Concretes;

namespace PArticle.Domain.Entities
{
	public class ArticleVote:EntityBase
	{
		public int ArticleId { get; set; }
		public DateTime VoteDate { get; set; }
		public string IpAddress { get; set; }
		public global::Domain.Contracts.Enums.ArticleVote Vote { get; set; }
		public virtual Article Article { get; set; }
	}
}
