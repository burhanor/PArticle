using PArticle.Application.Concretes;

namespace PArticle.Application.Features.Menu.Commands.DeleteMenuItem
{

	public class DeleteMenuItemCommandRequest : DeleteRequest
	{
		public DeleteMenuItemCommandRequest()
		{

		}
		public DeleteMenuItemCommandRequest(int id) : base(id)
		{
		}

		public DeleteMenuItemCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
