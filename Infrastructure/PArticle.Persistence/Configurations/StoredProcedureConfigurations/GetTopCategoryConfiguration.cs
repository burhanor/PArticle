using Microsoft.EntityFrameworkCore;

namespace PArticle.Persistence.Configurations.StoredProcedureConfigurations
{
	public class GetTopCategoryConfiguration : IEntityTypeConfiguration<Shared.Models.GetTopCategory>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Shared.Models.GetTopCategory> builder)
		{
			builder.HasNoKey().ToView(null);
		}

	}
}
