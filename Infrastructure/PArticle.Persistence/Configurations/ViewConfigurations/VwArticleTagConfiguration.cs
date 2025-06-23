using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	internal class VwArticleTagConfiguration:IEntityTypeConfiguration<VwArticleTag>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwArticleTag> builder)
		{
			builder.HasNoKey();
		}
	}
}
