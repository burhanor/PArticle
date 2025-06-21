using MediatR;
using PArticle.Application.Interfaces;
using PArticle.Application.Models;

namespace PArticle.Application.Concretes
{
	public  class DeleteRequest:IDeleteRequest,IRequest<ResponseContainer<Unit>>
	{
		public DeleteRequest()
		{
			
		}
		public DeleteRequest(int id)
		{
			Ids = [id];
		}
		public DeleteRequest(List<int> ids)
		{
			Ids = ids;
		}
		public List<int> Ids { get; set; } = [];
	}
}
