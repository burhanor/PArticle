using Domain.Contracts.Enums;
using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.User;

namespace PArticle.Application.Features.User.Queries.GetUsers
{
	public class GetUsersQueryRequest:UserFilterModel,IRequest<PaginationContainer<GetUsersQueryResponse>>
	{
	
	}
}
