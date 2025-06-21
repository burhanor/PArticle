using PArticle.Application.Concretes;

namespace PArticle.Application.Features.Tag.Commands.DeleteTag
{
	

	public class DeleteTagCommandRequest : DeleteRequest
	{
		public DeleteTagCommandRequest()
		{

		}
		public DeleteTagCommandRequest(int id) : base(id)
		{
		}

		public DeleteTagCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
