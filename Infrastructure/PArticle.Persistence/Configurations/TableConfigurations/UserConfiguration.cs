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
	internal class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.Nickname)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(u => u.EmailAddress)
				.IsRequired()
				.HasMaxLength(100);
			builder.Property(u => u.Password)
				.IsRequired()
				.HasMaxLength(256);
			builder.Property(u => u.UserType)
				.IsRequired();


		}
	}
}
