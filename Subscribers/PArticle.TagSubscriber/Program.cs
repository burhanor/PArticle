using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PArticle.Subscribers.Core.Concretes;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using PArticle.TagSubscriber;

var config = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();
using IHost host = Host.CreateDefaultBuilder(args)
				.ConfigureServices((context, services) =>
				{
					services.Configure<RabbitMqModel>(config.GetSection("RabbitMQ"));
					services.Configure<RedisModel>(config.GetSection("Redis"));
					services.AddSingleton<IRedisService, RedisService>();
					services.AddSingleton<IRabbitMqService, RabbitMqService>();
					services.AddSingleton<TagService>();
				})
				.Build();


var app = host.Services.GetRequiredService<TagService>();
await app.Run();

Console.ReadLine();
