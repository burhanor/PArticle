using AutoMapper;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.User.Commands.DeleteUser
{


	public class DeleteUserCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.User, DeleteUserCommandRequest>(uow, httpContextAccessor, mapper, Messages.User.DELETE_SUCCESS, Messages.User.DELETE_FAILED, rabbitMqService, Exchanges.User)
	{
		protected override async Task<bool> BeforeDeleteControl(List<int> deletedIds, CancellationToken cancellationToken)
		{
			bool hasArticle = await uow.GetReadRepository<Domain.Entities.Article>().ExistAsync(a => deletedIds.Contains(a.UserId), cancellationToken: cancellationToken);
			if (hasArticle)
			{
				return false; 
			}
			return true;
		}

	}
}
