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
	internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder.HasIndex(a => a.Slug)
				.IsUnique();
			builder.Property(a => a.Slug)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(a => a.Title)
				.IsRequired()
				.HasMaxLength(200);
			builder.Property(a => a.Content)
				.IsRequired();
			builder.Property(c => c.Status)
				.IsRequired();
			builder.HasOne(a => a.User)
				.WithMany(u => u.Articles)
				.HasForeignKey(a => a.UserId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
