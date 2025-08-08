using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PArticle.LogSubscriber;
using PArticle.Subscribers.Core.Concretes;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.RabbitMQ;

var config = new ConfigurationBuilder()
	.SetBasePath(AppContext.BaseDirectory)
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddUserSecrets<Program>(optional: true)
	.Build();

var rabbitMqModel = config.GetSection("RabbitMQ").Get<RabbitMqModel>();
var appConstantModel = config.GetSection("AppConstants").Get<AppConstantModel>();

if (rabbitMqModel is null || appConstantModel is null)
{
	Console.WriteLine("Config verileri alınamadı.");
	return;
}

var rabbitMqConfig = new RabbitMQClientConfiguration
{
	Port = rabbitMqModel.Port,
	Hostnames = new[] { rabbitMqModel.HostName },  
	AutoCreateExchange = false,
	Username = rabbitMqModel.User,
	Password = rabbitMqModel.Password,
	Exchange = appConstantModel.LogExchangeName,
	RoutingKey = appConstantModel.LogRoutingKey,
	ExchangeType = "direct",
	DeliveryMode = RabbitMQDeliveryMode.Durable
};

var rabbitMqSinkConfig = new RabbitMQSinkConfiguration
{
	TextFormatter = new CompactJsonFormatter()
};

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.WriteTo.RabbitMQ(rabbitMqConfig, rabbitMqSinkConfig)
	.CreateLogger();

using IHost host = Host.CreateDefaultBuilder(args)
	.UseWindowsService()
	.UseSerilog() 
	.ConfigureAppConfiguration(builder =>
	{
		builder
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddUserSecrets<Program>(optional: true);
	})
	.ConfigureServices((context, services) =>
	{
		services.Configure<RabbitMqModel>(config.GetSection("RabbitMQ"));
		services.Configure<RedisModel>(config.GetSection("Redis"));
		services.Configure<AppConstantModel>(config.GetSection("AppConstants"));
		services.Configure<ElasticSearchModel>(config.GetSection("ElasticSearch"));

		services.AddSingleton(typeof(IElasticSearchService<>), typeof(ElasticSearchService<>));
		services.AddSingleton<IRedisService, RedisService>();
		services.AddSingleton<IRabbitMqService, RabbitMqService>();

		services.AddHostedService<LogService>();
	})
	.Build();

await host.RunAsync();
