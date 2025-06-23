using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Entities;
using PArticle.Domain.Views;
using PArticle.Persistence.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
