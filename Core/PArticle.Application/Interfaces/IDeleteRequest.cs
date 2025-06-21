using MediatR;
using PArticle.Application.Models;

namespace PArticle.Application.Interfaces
{
	public interface IDeleteRequest : IRequest<ResponseContainer<Unit>>
	{
		public List<int> Ids { get; set; }
	}
}
