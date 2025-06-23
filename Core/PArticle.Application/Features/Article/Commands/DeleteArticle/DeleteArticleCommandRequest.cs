using PArticle.Application.Concretes;

namespace PArticle.Application.Features.Article.Commands.DeleteArticle
{
	public class DeleteArticleCommandRequest:DeleteRequest
	{
		public DeleteArticleCommandRequest()
		{
		}
		public DeleteArticleCommandRequest(int id) : base(id)
		{
		}
		public DeleteArticleCommandRequest(List<int> ids) : base(ids)
		{
		}
	}
}
