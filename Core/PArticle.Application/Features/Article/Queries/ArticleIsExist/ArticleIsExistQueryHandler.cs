using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using Domain.Contracts.Enums;

namespace PArticle.Application.Features.Article.Queries.ArticleIsExist
{
	public class ArticleIsExistQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Article>(uow, httpContextAccessor, mapper), IRequestHandler<ArticleIsExistQueryRequest, bool>
	{
		public async Task<bool> Handle(ArticleIsExistQueryRequest request, CancellationToken cancellationToken)
		{
			return await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Status==Status.Published, cancellationToken);
		}
	}
}
