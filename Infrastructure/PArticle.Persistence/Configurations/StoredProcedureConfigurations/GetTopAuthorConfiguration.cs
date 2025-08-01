using Microsoft.EntityFrameworkCore;

namespace PArticle.Persistence.Configurations.StoredProcedureConfigurations
{
	public class GetTopAuthorConfiguration : IEntityTypeConfiguration<Shared.Models.GetTopAuthor>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Shared.Models.GetTopAuthor> builder)
		{
			builder.HasNoKey().ToView(null);
		}

	}
}
