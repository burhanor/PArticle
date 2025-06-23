using Domain.Contracts.Enums;

namespace PArticle.Application.Models.Article
{
	public class ArticleCreateModel
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
		public List<string> Categories { get; set; } = [];
		public List<string> Tags { get; set; } = [];
	}
}
