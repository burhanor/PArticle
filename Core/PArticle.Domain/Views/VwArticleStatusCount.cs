using Domain.Contracts.Enums;
using Domain.Contracts.Interfaces;

namespace PArticle.Domain.Views
{
	public class VwArticleStatusCount : IViewBase
	{
		public Status Status { get; set; }
		public int Count { get; set; }
	}
}
