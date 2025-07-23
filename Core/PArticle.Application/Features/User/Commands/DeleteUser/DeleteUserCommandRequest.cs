using PArticle.Application.Concretes;

namespace PArticle.Application.Features.User.Commands.DeleteUser
{
	public class DeleteUserCommandRequest : DeleteRequest
	{
		public DeleteUserCommandRequest()
		{

		}
		public DeleteUserCommandRequest(int id) : base(id)
		{
		}

		public DeleteUserCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
