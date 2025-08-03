using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	
	internal class VwArticleViewDailyConfiguration : IEntityTypeConfiguration<VwArticleViewDaily>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwArticleViewDaily> builder)
		{
			builder.HasNoKey();
			builder.ToView("vwArticleViewDaily");
		}
	}
}
