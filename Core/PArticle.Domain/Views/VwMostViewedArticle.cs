using Domain.Contracts.Interfaces;

namespace PArticle.Domain.Views
{
	public class VwMostViewedArticle : IViewBase
	{
		public string Title { get; set; }
		public string Slug { get; set; }
		public long DisplayOrder { get; set; }
	}
}
