using Domain.Contracts.Enums;

namespace PArticle.Application.Models.Article
{
	public class ArticleFilterModel:FilterModel
	{
		public string? Title { get; set; }
		public string? Content { get; set; }
		public DateTime? PublishMinDate { get; set; }
		public DateTime? PublishMaxDate { get; set; }
		public string? Slug { get; set; }
		public int? UserId { get; set; }
		public Status? Status { get; set; }
		public List<int>? CategoryIds { get; set; }
		public List<int>? TagIds { get; set; }
	}
}
