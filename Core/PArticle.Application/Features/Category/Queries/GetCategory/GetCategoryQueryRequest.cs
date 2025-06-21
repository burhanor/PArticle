using PArticle.Application.Concretes;

namespace PArticle.Application.Features.Category.Queries.GetCategory
{
	public class GetCategoryQueryRequest(int id) : GetByIdRequest<GetCategoryQueryResponse>(id)
	{
	}
}
