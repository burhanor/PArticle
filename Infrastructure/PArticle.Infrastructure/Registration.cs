﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PArticle.Application.Abstractions.Interfaces.ElasticSearch;
using PArticle.Application.Abstractions.Interfaces.FileStorage;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using PArticle.Application.Abstractions.Interfaces.Redis;
using PArticle.Application.Abstractions.Interfaces.Token;
using PArticle.Infrastructure.ElasticSearch;
using PArticle.Infrastructure.FileStorage;
using PArticle.Infrastructure.RabbitMq;
using PArticle.Infrastructure.Redis;
using PArticle.Infrastructure.Token;
using System.Text;

namespace PArticle.Infrastructure
{
	public static class Registration
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<TokenModel>(configuration.GetSection("JWT"));
			services.AddTransient<ITokenService, TokenService>();
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
			{
				opt.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Cookies["accessToken"];
						if (!string.IsNullOrEmpty(accessToken))
						{
							context.Token = accessToken;
						}
						return Task.CompletedTask;
					}
				};
				//TODO: Ayarlar yapılacak
				opt.SaveToken = true;
				opt.TokenValidationParameters = new()
				{
					ValidateIssuerSigningKey = true,

					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = false,
					ValidIssuer = configuration["JWT:Issuer"],
					ValidAudience = configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"] ?? string.Empty)),
					ClockSkew = TimeSpan.Zero
				};
			});

			services.Configure<RabbitMqModel>(configuration.GetSection("RabbitMQ"));
			services.AddSingleton<IRabbitMqService, RabbitMqService>();

			services.Configure<RedisModel>(configuration.GetSection("Redis"));
			services.AddSingleton<IRedisService, RedisService>();

			services.Configure<ElasticSearchModel>(configuration.GetSection("ElasticSearch"));
			services.AddSingleton(typeof(IElasticSearchService<>),typeof(ElasticSearchService<>));
			services.AddScoped<IFileStorageService, FileStorageService>();

		}

	}
}
