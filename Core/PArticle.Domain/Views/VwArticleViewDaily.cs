using Domain.Contracts.Interfaces;

namespace PArticle.Domain.Views
{
	public class VwArticleViewDaily : IViewBase
	{
		public DateTime ViewDay { get; set; }
		public int TotalViews { get; set; }
		public int UniqueViews { get; set; }
	}
}
