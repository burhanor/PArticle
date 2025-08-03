using MediatR;

namespace PArticle.Application.Features.Stats.Queries.GetVisitors
{
	public class GetVisitorsQueryRequest(DateTime startDate, DateTime endDate) : IRequest<IList<GetVisitorsQueryResponse>>	
	{
		public DateTime StartDate { get; set; } = startDate;
		public DateTime EndDate { get; set; } = endDate;
	}
}
