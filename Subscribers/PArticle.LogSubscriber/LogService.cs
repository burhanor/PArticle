using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PArticle.LogSubscriber.Model;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using Serilog;

namespace PArticle.LogSubscriber
{
	public class LogService : BackgroundService
	{
		private readonly IRedisService _redisService;
		private readonly IRabbitMqService _rabbitMqService;
		private readonly IElasticSearchService<ElasticLogModel> _elasticSearchService;
		private readonly AppConstantModel _acm;

		public LogService(
			IRedisService redisService,
			IRabbitMqService rabbitMqService,
			IElasticSearchService<ElasticLogModel> elasticSearchService,
			IOptions<AppConstantModel> options)
		{
			_redisService = redisService;
			_rabbitMqService = rabbitMqService;
			_elasticSearchService = elasticSearchService;
			_acm = options.Value;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				Log.Information("{AppName} başlatıldı.", _acm.AppName);

				await _rabbitMqService.Subscribe(
					_acm.QueueName,
					ElasticSearch_AddOrUpdateAsync,
					ElasticSearch_DeleteAsync,
					stoppingToken);

				// Servisin kapanmaması için döngü
				while (!stoppingToken.IsCancellationRequested)
				{
					await Task.Delay(1000, stoppingToken);
				}
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "{AppName}: ExecuteAsync sırasında kritik hata.", _acm.AppName);
			}
		}

		public async Task<bool> ElasticSearch_AddOrUpdateAsync(string message)
		{
			try
			{
				if (string.IsNullOrEmpty(message))
					return true;

				var log = new ElasticLogModel(message);
				await _elasticSearchService.UpsertAsync(log.ElasticId, log);
				return true;
			}
			catch (Exception ex)
			{
				Log.Error(ex, "{AppName}: ElasticSearch_AddOrUpdateAsync sırasında hata oluştu. Mesaj: {Message}", _acm.AppName, message);
				return false;
			}
		}

		public async Task<bool> ElasticSearch_DeleteAsync(string message)
		{
			// Burada silme işlemi yoksa true dönebilir veya ihtiyaca göre implement edebilirsin.
			return await Task.FromResult(true);
		}
	}
}
