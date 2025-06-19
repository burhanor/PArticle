using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PArticle.Application.Abstractions.Interfaces.Repositories;
using PArticle.Application.Abstractions.Interfaces.Uow;
using PArticle.Persistence.Context;
using PArticle.Persistence.Repositories.Concretes;
using PArticle.Persistence.UnitOfWork;

namespace PArticle.Persistence
{
	public static class Registration
	{
		public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ArticleDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"));
			});

			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
			services.AddScoped<IUow, Uow>();

		}
	}
}
