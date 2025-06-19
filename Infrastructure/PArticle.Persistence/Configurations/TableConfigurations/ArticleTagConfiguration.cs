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
	internal class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
	{
		public void Configure(EntityTypeBuilder<ArticleTag> builder)
		{
			builder.HasIndex(ac => new { ac.ArticleId, ac.TagId })
				.IsUnique();

			builder.HasOne(ac => ac.Article)
				.WithMany(a => a.ArticleTags)
				.HasForeignKey(ac => ac.ArticleId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(ac => ac.Tag)
				.WithMany(c => c.ArticleTags)
				.HasForeignKey(ac => ac.TagId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
