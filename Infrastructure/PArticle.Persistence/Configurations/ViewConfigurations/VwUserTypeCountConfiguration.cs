using Microsoft.EntityFrameworkCore;
using PArticle.Domain.Views;

namespace PArticle.Persistence.Configurations.ViewConfigurations
{
	

	internal class VwUserTypeCountConfiguration : IEntityTypeConfiguration<VwUserTypeCount>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VwUserTypeCount> builder)
		{
			builder.HasNoKey();
			builder.ToView("vwUserTypeCount");
		}
	}
}
