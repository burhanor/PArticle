using AutoMapper;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Helpers.FeatureHelpers;

namespace PArticle.Application.Features.Category.Commands.DeleteCategory
{
	public class DeleteCategoryCommandHandler(IUow uow,IHttpContextAccessor httpContextAccessor,IMapper mapper,IRabbitMqService rabbitMqService):DeleteHandler<Domain.Entities.Category, DeleteCategoryCommandRequest>(uow,httpContextAccessor,mapper,Messages.Category.CATEGORY_DELETE_SUCCESS, Messages.Category.CATEGORY_DELETE_FAILED, rabbitMqService,Exchanges.Category)
	{
		protected override async Task AfterDeleteSuccessAsync( List<int> deletedIds, CancellationToken cancellationToken)
		{
			IList<int> articleIds = await uow.GetReadRepository<Domain.Entities.ArticleCategory>().GetListAsync
				   (
				   predicate: m => deletedIds.Contains(m.CategoryId),
				   select: m => m.ArticleId,
				   cancellationToken: cancellationToken
				   );
			await uow.SaveChangesAsync(cancellationToken);
			await ArticleHelper.UpdateArticles(articleIds, uow, httpContextAccessor, mapper, rabbitMqService, cancellationToken);
		}
	}
}
