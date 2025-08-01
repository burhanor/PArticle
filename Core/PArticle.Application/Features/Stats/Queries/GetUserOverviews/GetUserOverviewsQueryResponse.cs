using Domain.Contracts.Enums;

namespace PArticle.Application.Features.Stats.Queries.GetUserOverviews
{
	public class GetUserOverviewsQueryResponse
	{
		public bool IsActive { get; set; }
		public UserType UserType { get; set; }
		public int Count { get; set; }
	}
}
