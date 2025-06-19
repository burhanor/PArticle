using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PArticle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Persistence.Configurations.TableConfigurations
{
	internal class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
	{
		public void Configure(EntityTypeBuilder<MenuItem> builder)
		{
			builder.Property(m => m.Title)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(m => m.Description)
				.HasMaxLength(100);
			builder.Property(m => m.Link)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(m => m.MenuType)
				.IsRequired();
		}
	}
}
