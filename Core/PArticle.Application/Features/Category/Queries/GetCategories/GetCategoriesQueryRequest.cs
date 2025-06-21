using MediatR;
using PArticle.Application.Models;
using PArticle.Domain.Enums;

namespace PArticle.Application.Features.Category.Queries.GetCategories
{
	public class GetCategoriesQueryRequest:FilterModel,IRequest<PaginationContainer<GetCategoriesQueryResponse>>
	{
		public string? Name { get; set; }
		public string? Slug { get; set; }
		public Status? Status { get; set; }
	}
}
