using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	
	internal class VwTopRatedArticleConfiguration : IEntityTypeConfiguration<VwTopRatedArticle>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwTopRatedArticle> builder)
		{
			builder.HasNoKey();
			builder.ToView("vwTopRatedArticles");
		}
	}
}
