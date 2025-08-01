using Microsoft.EntityFrameworkCore;

namespace PArticle.Persistence.Configurations.StoredProcedureConfigurations
{
	public class GetTopTagConfiguration : IEntityTypeConfiguration<Shared.Models.GetTopTag>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Shared.Models.GetTopTag> builder)
		{
			builder.HasNoKey().ToView(null);
		}

	}
}
