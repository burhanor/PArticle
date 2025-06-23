using Domain.Contracts.Enums;
using Domain.Contracts.Interfaces;

namespace PArticle.Domain.Views
{
	public class VwArticleTag:IViewBase
	{
		public int ArticleId { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
	}
}
