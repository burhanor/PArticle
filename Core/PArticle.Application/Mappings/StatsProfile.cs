using AutoMapper;
using PArticle.Application.Features.Stats.Queries.GetArticleOverviews;
using PArticle.Application.Features.Stats.Queries.GetArticleRates;
using PArticle.Application.Features.Stats.Queries.GetCategoryOverviews;
using PArticle.Application.Features.Stats.Queries.GetTagOverviews;
using PArticle.Application.Features.Stats.Queries.GetTopArticles;
using PArticle.Application.Features.Stats.Queries.GetTopAuthors;
using PArticle.Application.Features.Stats.Queries.GetTopCategories;
using PArticle.Application.Features.Stats.Queries.GetTopTags;
using PArticle.Application.Features.Stats.Queries.GetUserOverviews;
using PArticle.Domain.Views;

namespace PArticle.Application.Mappings
{
	public class StatsProfile : Profile
	{
		public StatsProfile()
		{

			CreateMap<VwArticleStatusCount, GetArticleOverviewsQueryResponse>();
			CreateMap<VwCategoryStatusCount, GetCategoryOverviewsQueryResponse>();
			CreateMap<VwTagStatusCount, GetTagOverviewsQueryResponse>();
			CreateMap<VwUserTypeCount, GetUserOverviewsQueryResponse>();
			CreateMap<Shared.Models.GetTopTag, GetTopTagsQueryResponse>();
			CreateMap<Shared.Models.GetTopCategory, GetTopCategoriesQueryResponse>();
			CreateMap<Shared.Models.GetTopAuthor, GetTopAuthorsQueryResponse>();
			CreateMap<Shared.Models.GetTopArticle, GetTopArticlesQueryResponse>();
			CreateMap<Shared.Models.GetArticleRate, GetArticleRatesQueryResponse>();
		}
	}
}
