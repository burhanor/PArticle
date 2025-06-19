﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PArticle.Application
{
	public static class Registration
	{
		public static void AddApplication(this IServiceCollection services)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
			services.AddAutoMapper(assembly);

		}
	}
}
