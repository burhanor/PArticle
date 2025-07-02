using Domain.Contracts.Interfaces;

namespace PArticle.Domain.Views
{
	public class VwTopRatedArticle:IViewBase
	{
		public string Title { get; set; }
		public string Slug { get; set; }
		public long DisplayOrder { get; set; }
	}
}
