using AutoMapper;
using PArticle.Application.Features.Article.Commands.CreateArticle;
using PArticle.Application.Features.Article.Commands.InsertArticleView;
using PArticle.Application.Features.Article.Commands.UpdateArticle;
using PArticle.Application.Features.Article.Commands.UpsertArticleVote;
using PArticle.Application.Features.Article.Queries.GetArticle;
using PArticle.Application.Features.Article.Queries.GetArticleByAuthor;
using PArticle.Application.Features.Article.Queries.GetArticleByCategory;
using PArticle.Application.Features.Article.Queries.GetArticleDetail;
using PArticle.Application.Features.Article.Queries.GetArticles;
using PArticle.Application.Features.Article.Queries.GetArticleVotes;
using PArticle.Application.Features.Article.Queries.GetMostViewedArticles;
using PArticle.Application.Features.Article.Queries.GetTopRatedArticles;
using PArticle.Application.Features.Category.Queries.GetCategories;
using PArticle.Application.Models.Article;
using PArticle.Application.Models.Category;
using PArticle.Application.Models.Tag;
using PArticle.Domain.Views;

namespace PArticle.Application.Mappings
{
	public class ArticleProfile:Profile
	{
		public ArticleProfile()
		{
			CreateMap<CreateArticleCommandRequest, Domain.Entities.Article>();
			CreateMap<ArticleCreateModel, CreateArticleCommandRequest>();
			CreateMap<Domain.Entities.Article, CreateArticleCommandResponse>();
			CreateMap<GetArticleQueryResponse,CreateArticleCommandResponse>();


			CreateMap<UpdateArticleCommandRequest, Domain.Entities.Article>();
			CreateMap<ArticleUpdateModel, UpdateArticleCommandRequest>();
			CreateMap<Domain.Entities.Article, UpdateArticleCommandResponse>();
			CreateMap<GetArticleQueryResponse, UpdateArticleCommandResponse>();


			CreateMap<Domain.Entities.Article, GetArticleQueryResponse>();
			CreateMap<VwArticleTag, TagDto>();
			CreateMap<VwArticleCategory, CategoryDto>();

			CreateMap<Domain.Entities.Article, GetArticlesQueryResponse>();
			CreateMap<ArticleModel, GetArticlesQueryResponse>();
			CreateMap<ArticleFilterModel, GetArticlesQueryRequest>();

			CreateMap<InsertArticleViewCommandRequest,Domain.Entities.ArticleView>();

			CreateMap<VwArticleVote,GetArticleVotesQueryResponse>();
			CreateMap<UpsertArticleVoteCommandRequest, Domain.Entities.ArticleVote>();


			CreateMap<VwMostViewedArticle,GetMostViewedArticlesQueryResponse>();
			CreateMap<VwTopRatedArticle, GetTopRatedArticlesQueryResponse>();

			CreateMap<ArticleModel, GetArticleByCategoryQueryResponse>();
			CreateMap<ArticleModel, GetArticleByAuthorQueryResponse>();
			
			CreateMap<Domain.Entities.Article, GetArticleDetailQueryResponse>();
			
		}
	}
}
