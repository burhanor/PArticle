using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{

	internal class VwArticleVoteConfiguration : IEntityTypeConfiguration<VwArticleVote>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwArticleVote> builder)
		{
			builder.HasNoKey();
			builder.ToView("vwArticleVotes");
		}
	}
}
