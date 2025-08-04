namespace PArticle.Application.Features.Article.Queries.GetArticleInfo
{
	public class GetArticleInfoQueryResponse
	{
		public int ArticleId { get; set; }
		public int LikeCount { get; set; }
		public int DislikeCount { get; set; }
		public int ViewCount { get; set; }
	}
}
