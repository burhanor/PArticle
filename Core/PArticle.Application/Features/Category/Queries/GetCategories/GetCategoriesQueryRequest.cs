using Domain.Contracts.Enums;
using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Features.Category.Queries.GetCategories
{
	public class GetCategoriesQueryRequest:FilterModel,IRequest<PaginationContainer<GetCategoriesQueryResponse>>
	{
		public string? Name { get; set; }
		public string? Slug { get; set; }
		public Status? Status { get; set; }
	}
}
