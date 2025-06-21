using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;

namespace PArticle.Application.Features.Category.Queries.GetCategory
{
	public class GetCategoryQueryHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper) : BaseHandler<Domain.Entities.Category>(uow, httpContextAccessor, mapper), IRequestHandler<GetCategoryQueryRequest, GetCategoryQueryResponse?>
	{
		public async Task<GetCategoryQueryResponse?> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
		{
			GetCategoryQueryResponse response = new();
			Domain.Entities.Category? category = await readRepository.FindAsync(request.Id,cancellationToken:cancellationToken);
			if (category!=null)
			{
				response = mapper.Map<GetCategoryQueryResponse>(category);
				return response;
			}
			return null;

		}
	}
}
