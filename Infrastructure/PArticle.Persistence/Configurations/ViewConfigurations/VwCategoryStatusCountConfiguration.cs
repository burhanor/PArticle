using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	

	internal class VwCategoryStatusCountConfiguration : IEntityTypeConfiguration<VwCategoryStatusCount>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwCategoryStatusCount> builder)
		{
			builder.HasNoKey();
			builder.ToView("vwCategoryStatusCount");
		}
	}
}
