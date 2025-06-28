using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Article.Queries.GetArticleViewCount
{
	public class GetArticleViewCountQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler(uow, httpContextAccessor, mapper), IRequestHandler<GetArticleViewCountQueryRequest, GetArticleViewCountQueryResponse>
	{
		public async Task<GetArticleViewCountQueryResponse> Handle(GetArticleViewCountQueryRequest request, CancellationToken cancellationToken)
		{
			GetArticleViewCountQueryResponse response = new();
		    Domain.Views.VwArticleView? viewCount = uow.GetViewRepository<Domain.Views.VwArticleView>().Query().FirstOrDefault(m => m.ArticleId == request.ArticleId);
			if (viewCount != null)
				response.ViewCount = viewCount.TotalViewCount;

			return response;
		}
	}
}
