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
	internal class ArticleViewConfiguration : IEntityTypeConfiguration<ArticleView>
	{
		public void Configure(EntityTypeBuilder<ArticleView> builder)
		{
			builder.Property(av => av.ArticleId)
				.IsRequired();
			builder.Property(av => av.IpAddress)
				.IsRequired()
				.HasMaxLength(45); 
			builder.HasOne(av => av.Article)
				.WithMany(a => a.ArticleViews)
				.HasForeignKey(av => av.ArticleId);
		}
	}
}
