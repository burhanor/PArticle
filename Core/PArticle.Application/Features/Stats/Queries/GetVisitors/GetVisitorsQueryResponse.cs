namespace PArticle.Application.Features.Stats.Queries.GetVisitors
{
	public class GetVisitorsQueryResponse
	{
		public DateTime ViewDay { get; set; }
		public int TotalViews { get; set; }
		public int UniqueViews { get; set; }
	}
}
