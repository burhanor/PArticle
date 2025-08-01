using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	

	internal class VwTagStatusCountConfiguration : IEntityTypeConfiguration<VwTagStatusCount>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwTagStatusCount> builder)
		{
			builder.HasNoKey();
			builder.ToView("vwTagStatusCount");
		}
	}
}
