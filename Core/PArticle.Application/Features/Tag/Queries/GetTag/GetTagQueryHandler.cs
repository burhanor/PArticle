using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Features.Tag.Queries.GetTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Tag.Queries.GetTag
{
	

	public class GetTagQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : BaseHandler<Domain.Entities.Tag>(uow, httpContextAccessor, mapper, rabbitMqService), IRequestHandler<GetTagQueryRequest, GetTagQueryResponse?>
	{
		public async Task<GetTagQueryResponse?> Handle(GetTagQueryRequest request, CancellationToken cancellationToken)
		{
			GetTagQueryResponse response = new();
			Domain.Entities.Tag? tag = await readRepository.FindAsync(request.Id, cancellationToken: cancellationToken);
			if (tag != null)
			{
				response = mapper.Map<GetTagQueryResponse>(tag);
				return response;
			}
			return null;

		}
	}
}
