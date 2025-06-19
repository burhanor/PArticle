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
	internal class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
	{
		public void Configure(EntityTypeBuilder<UserLogin> builder)
		{
			builder.Property(ul => ul.IpAddress)
				.IsRequired()
				.HasMaxLength(45);

			builder.Property(ul => ul.UserId)
				.IsRequired();

			builder.HasOne(ul => ul.User)
				.WithMany(u => u.UserLogins)
				.HasForeignKey(ul => ul.UserId);

		}
	}
}
