using AutoMapper;
using Microsoft.AspNetCore.Http;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Application.Bases;
using PArticle.Application.Constants;
using PArticle.Application.Helpers.FeatureHelpers;

namespace PArticle.Application.Features.Tag.Commands.DeleteTag
{
	

	public class DeleteTagCommandHandler(IUow uow, IHttpContextAccessor httpContextAccessor, IMapper mapper,IRabbitMqService rabbitMqService) : DeleteHandler<Domain.Entities.Tag, DeleteTagCommandRequest>(uow, httpContextAccessor, mapper, Messages.Tag.TAG_DELETE_SUCCESS, Messages.Tag.TAG_DELETE_FAILED,rabbitMqService,Exchanges.Tag)
	{

		protected override async Task AfterDeleteSuccessAsync( List<int> deletedIds, CancellationToken cancellationToken)
		{
			IList<int> articleIds = await uow.GetReadRepository<Domain.Entities.ArticleTag>().GetListAsync
				   (
				   predicate: m => deletedIds.Contains(m.TagId),
				   select: m => m.ArticleId,
				   cancellationToken: cancellationToken
				   );
			await uow.SaveChangesAsync(cancellationToken);
			await ArticleHelper.UpdateArticles(articleIds, uow, httpContextAccessor, mapper, rabbitMqService, cancellationToken);
		}
	}
}
