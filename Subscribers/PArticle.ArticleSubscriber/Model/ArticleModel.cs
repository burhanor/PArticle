using PArticle.ArticleSubscriber.Enums;
using PArticle.Subscribers.Core.Interfaces;

namespace PArticle.ArticleSubscriber.Model
{
	public class ArticleModel:IElasticEntity
	{
		public string ElasticId { get; set; } = Guid.NewGuid().ToString();
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime? PublishDate { get; set; }
		public string Slug { get; set; }
		public int UserId { get; set; }
		public Status Status { get; set; }
		public List<CategoryDto> Categories { get; set; } = [];
		public List<TagDto> Tags { get; set; } = [];
	}
}
