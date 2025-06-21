using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PArticle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Persistence.Configurations.TableConfigurations
{
	internal class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
	{
		public void Configure(EntityTypeBuilder<ArticleCategory> builder)
		{
			builder.HasIndex(ac => new { ac.ArticleId, ac.CategoryId })
				.IsUnique();

			builder.HasOne(ac => ac.Article)
				.WithMany(a => a.ArticleCategories)
				.HasForeignKey(ac => ac.ArticleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(ac => ac.Category)
				.WithMany(c => c.ArticleCategories)
				.HasForeignKey(ac => ac.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
