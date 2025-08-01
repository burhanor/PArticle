using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	

	internal class VwArticleStatusCountConfiguration : IEntityTypeConfiguration<VwArticleStatusCount>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwArticleStatusCount> builder)
		{
			builder.HasNoKey();
			builder.ToView("vwArticleStatusCount");
		}
	}
}
