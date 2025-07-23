using MediatR;
using PArticle.Application.Abstractions.Interfaces;
using PArticle.Application.Models;
using PArticle.Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.User.Commands.UpdateUser
{
	public class UpdateUserCommandRequest:UserRequestModel,IId,IRequest<ResponseContainer<UpdateUserCommandResponse>>
	{
	}
}
