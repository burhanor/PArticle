using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PArticle.Domain.Entities;
using PArticle.Persistence.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Persistence.Configurations.TableConfigurations
{
	internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(c => c.Slug)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(c => c.Status)
				.IsRequired();
			builder.HasIndex(c => c.Slug)
				.IsUnique();
			builder.HasIndex(c => c.Name)
				.IsUnique();
			builder.HasData(CategorySeed.Seed());
		}
	}
}
