using Domain.Contracts.Enums;
using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Tag.Queries.GetTags
{


	public class GetTagsQueryRequest : FilterModel, IRequest<PaginationContainer<GetTagsQueryResponse>>
	{
		public string? Name { get; set; }
		public string? Slug { get; set; }
		public Status? Status { get; set; }
	}
}
