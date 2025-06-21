using PArticle.Application.Concretes;

namespace PArticle.Application.Features.Category.Commands.DeleteCategory
{
	public class DeleteCategoryCommandRequest : DeleteRequest
	{
		public DeleteCategoryCommandRequest()
		{

		}
		public DeleteCategoryCommandRequest(int id) : base(id)
		{
		}

		public DeleteCategoryCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
