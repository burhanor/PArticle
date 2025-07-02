using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	
	internal class VwMostViewedArticleConfiguration : IEntityTypeConfiguration<VwMostViewedArticle>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwMostViewedArticle> builder)
		{
			builder.HasNoKey();
			builder.ToView("vwMostViewedArticles");
		}
	}
}
