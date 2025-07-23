using AutoMapper;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Features.Tag.Commands.DeleteTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.User.Commands.DeleteUser
{


	public class DeleteUserCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper, IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.User, DeleteUserCommandRequest>(uow, httpContextAccessor, mapper, Messages.User.DELETE_SUCCESS, Messages.User.DELETE_FAILED, rabbitMqService, Exchanges.User)
	{
	}
}
