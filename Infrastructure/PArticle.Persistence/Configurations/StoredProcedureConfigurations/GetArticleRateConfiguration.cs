using Microsoft.EntityFrameworkCore;

namespace PArticle.Persistence.Configurations.StoredProcedureConfigurations
{
	public class GetArticleRateConfiguration : IEntityTypeConfiguration<Shared.Models.GetArticleRate>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Shared.Models.GetArticleRate> builder)
		{
			builder.HasNoKey().ToView(null);
		}

	}
}
