using MediatR;

namespace PArticle.Application.Features.Category.Queries.CategoryIsExist
{
	

	public class CategoryIsExistQueryRequest(string slug) : IRequest<bool>
	{
		public string Slug { get; set; } = slug;
	}
}
