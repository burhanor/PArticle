using MediatR;
using PArticle.Application.Abstractions.Interfaces;
using PArticle.Application.Models;
using PArticle.Application.Models.Tag;

namespace PArticle.Application.Features.Tag.Commands.UpdateTag
{
	public class UpdateTagCommandRequest:TagDto,IRequest<ResponseContainer<UpdateTagCommandResponse>>, IId
	{
		public int Id { get; set; }
	}
}
