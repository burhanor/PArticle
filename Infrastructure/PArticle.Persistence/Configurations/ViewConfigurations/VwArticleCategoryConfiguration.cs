using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	
	internal class VwArticleCategoryConfiguration : IEntityTypeConfiguration<VwArticleCategory>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwArticleCategory> builder)
		{
			builder.HasNoKey();
		}
	}
}
