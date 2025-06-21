using PArticle.Domain.Concretes;
using PArticle.Domain.Enums;

namespace PArticle.Domain.Entities
{
	public class Article:EntityBase
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime PublishDate { get; set; }
		public string Slug { get; set; }
		public int UserId { get; set; }
		public Status Status { get; set; }

		public virtual User User { get; set; }
		public virtual ICollection<ArticleVote> ArticleVotes { get; set; }
		public virtual ICollection<ArticleView> ArticleViews { get; set; }

		public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
		public virtual ICollection<ArticleTag> ArticleTags { get; set; }

	}
}
