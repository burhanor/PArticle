using Domain.Contracts.Interfaces;

namespace PArticle.Domain.Views
{
	public class VwArticleView : IViewBase
	{
		public int ArticleId { get; set; }
		public int TotalViewCount { get; set; }
	}
}
