using MediatR;
using PArticle.Application.Models;
using PArticle.Application.Models.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Tag.Commands.CreateTag
{
	public class CreateTagCommandRequest:TagCreateModel,IRequest<ResponseContainer<CreateTagCommandResponse>>
	{
	}
}
