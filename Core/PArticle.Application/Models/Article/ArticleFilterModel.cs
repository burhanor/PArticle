using Domain.Contracts.Enums;

namespace PArticle.Application.Models.Article
{
	public class ArticleFilterModel:FilterModel
	{
		public string? SearchKey { get; set; }
		public Status? Status { get; set; }

	}
}
