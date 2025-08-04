using AutoMapper;
using Domain.Contracts.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Category.Queries.CategoryIsExist
{
	

	public class CategoryIsExistQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Category>(uow, httpContextAccessor, mapper), IRequestHandler<CategoryIsExistQueryRequest, bool>
	{
		public async Task<bool> Handle(CategoryIsExistQueryRequest request, CancellationToken cancellationToken)
		{
			return await readRepository.ExistAsync(x => x.Slug == request.Slug && x.Status == Status.Published, cancellationToken);
		}
	}
}
