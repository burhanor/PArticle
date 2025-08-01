using Microsoft.EntityFrameworkCore;

namespace PArticle.Persistence.Configurations.StoredProcedureConfigurations
{
	public class GetTopArticleConfiguration : IEntityTypeConfiguration<Shared.Models.GetTopArticle>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Shared.Models.GetTopArticle> builder)
		{
			builder.HasNoKey().ToView(null);
		}

	}
}
