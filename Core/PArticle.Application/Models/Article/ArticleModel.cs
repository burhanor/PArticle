using Domain.Contracts.Enums;
using PArticle.Application.Models.Category;
using PArticle.Application.Models.Tag;

namespace PArticle.Application.Models.Article
{
	public class ArticleModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? PublishDate { get; set; }
		public string Slug { get; set; }
		public int UserId { get; set; }
		public Status Status { get; set; }
		public List<CategoryDto> Categories { get; set; } = [];
		public List<TagDto> Tags { get; set; } = [];
		public string? Nickname { get; set; }
	}
}
