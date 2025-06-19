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
	internal class TagConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(t => t.Slug)
				.IsRequired()
				.HasMaxLength(50);
			builder.HasIndex(t => t.Slug)
				.IsUnique();
			builder.HasIndex(t => t.Name)
				.IsUnique();
			builder.Property(t => t.Status)
				.IsRequired();
			builder.HasData(TagSeed.Seed());

		}
	}
}
