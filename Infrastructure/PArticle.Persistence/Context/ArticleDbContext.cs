using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Entities;
using PArticle.Domain.Views;

namespace PArticle.Persistence.Context
{
	public class ArticleDbContext(DbContextOptions<ArticleDbContext> options):DbContext(options)	
	{
		#region Tables

		public DbSet<Article> Articles { get; set; }
		public DbSet<ArticleCategory> ArticleCategories { get; set; }
		public DbSet<ArticleTag> ArticleTags { get; set; }
		public DbSet<ArticleView> ArticleViews { get; set; }
		public DbSet<ArticleVote> ArticleVotes { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<MenuItem> MenuItems { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<User> Users { get; set; }

		public DbSet<UserLogin> UserLogins { get; set; }

		#endregion

		#region Views

		public DbSet<VwArticleCategory> VwArticleCategories { get; set; }
		public DbSet<VwArticleTag> VwArticleTags { get; set; }
		public DbSet<VwArticleView> VwArticleViews { get; set; }
		public DbSet<VwArticleVote> VwArticleVotes { get; set; }
		public DbSet<VwTopRatedArticle> VwTopRatedArticles { get; set; }
		public DbSet<VwMostViewedArticle> VwMostViewedArticles { get; set; }
		public DbSet<VwArticleStatusCount> VwArticleStatusCounts { get; set; }
		public DbSet<VwTagStatusCount> VwTagStatusCounts { get; set; }
		public DbSet<VwCategoryStatusCount> VwCategoryStatusCounts { get; set; }
		public DbSet<VwUserTypeCount> VwUserTypeCounts { get; set; }
		public DbSet<VwArticleViewDaily> VwArticleViewDaily { get; set; }

		#endregion


		#region Stored Procedures
		public DbSet<PArticle.Shared.Models.GetTopTag> GetTopTags { get; set; }
		public DbSet<PArticle.Shared.Models.GetTopCategory> GetTopCategories { get; set; }
		public DbSet<PArticle.Shared.Models.GetTopAuthor> GetTopAuthors { get; set; }
		public DbSet<PArticle.Shared.Models.GetTopArticle> GetTopArticles { get; set; }
		public DbSet<PArticle.Shared.Models.GetArticleRate> GetArticleRates { get; set; }

		#endregion


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleDbContext).Assembly);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
	}
}
