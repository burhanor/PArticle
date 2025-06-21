using PArticle.Application.Concretes;

namespace PArticle.Application.Features.Tag.Queries.GetTag
{
	public class GetTagQueryRequest(int id) : GetByIdRequest<GetTagQueryResponse>(id)
	{

	}
}
