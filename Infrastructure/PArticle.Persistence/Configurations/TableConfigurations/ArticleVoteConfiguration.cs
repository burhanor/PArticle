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
	internal class ArticleVoteConfiguration : IEntityTypeConfiguration<ArticleVote>
	{
		public void Configure(EntityTypeBuilder<ArticleVote> builder)
		{
			builder.Property(av => av.ArticleId)
				.IsRequired();
			builder.Property(av => av.IpAddress)
				.IsRequired()
				.HasMaxLength(45); // IPv6 support
			builder.HasOne(av => av.Article)
				.WithMany(a => a.ArticleVotes)
				.HasForeignKey(av => av.ArticleId);
		}
	}
}
