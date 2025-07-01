using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PArticle.MenuItemSubscriber;
using PArticle.Subscribers.Core.Concretes;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.RabbitMQ;


var config = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();

RabbitMqModel? rabbitMqModel = config.GetSection("RabbitMQ").Get<RabbitMqModel>();
var appConstant = config.GetSection("AppConstants");
AppConstantModel? appConstantModel = appConstant.Get<AppConstantModel>();
if (rabbitMqModel is null)
{
	Console.WriteLine("RabbitMQ ayarları bulunamadı. Lütfen appsettings.json dosyasını kontrol edin.");
	return;
}
if (appConstantModel is null)
{
	Console.WriteLine("AppConstants ayarları bulunamadı. Lütfen appsettings.json dosyasını kontrol edin.");
	return;
}

using IHost host = Host.CreateDefaultBuilder(args)
				.ConfigureServices((context, services) =>
				{
					services.Configure<RabbitMqModel>(config.GetSection("RabbitMQ"));
					services.Configure<RedisModel>(config.GetSection("Redis"));
					services.Configure<AppConstantModel>(appConstant);
					services.AddSingleton<IRedisService, RedisService>();
					services.AddSingleton<IRabbitMqService, RabbitMqService>();
					services.AddSingleton<MenuItemService>();
				})
				.Build();


var rabbitMqConfig = new RabbitMQClientConfiguration
{
	Port = rabbitMqModel.Port,
	Hostnames = [rabbitMqModel.HostName],
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


var app = host.Services.GetRequiredService<MenuItemService>();

await app.LogInit();
Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.RabbitMQ(rabbitMqConfig, rabbitMqSinkConfig)
			.CreateLogger();
await app.Run();

Console.ReadLine();
