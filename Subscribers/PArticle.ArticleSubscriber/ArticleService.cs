using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PArticle.ArticleSubscriber.Model;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using Serilog;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PArticle.ArticleSubscriber
{
	public class ArticleService : BackgroundService
	{
		private readonly IRedisService redisService;
		private readonly IRabbitMqService rabbitMqService;
		private readonly IElasticSearchService<ArticleModel> elasticSearchService;
		private readonly AppConstantModel acm;

		public ArticleService(
			IRedisService redisService,
			IRabbitMqService rabbitMqService,
			IElasticSearchService<ArticleModel> elasticSearchService,
			IOptions<AppConstantModel> options)
		{
			this.redisService = redisService;
			this.rabbitMqService = rabbitMqService;
			this.elasticSearchService = elasticSearchService;
			this.acm = options.Value;
		}

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			Log.Information($"{acm.AppName} background servis olarak başlatıldı.");
			await LogInit();
			await base.StartAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				// Burada RabbitMQ aboneliğini başlatıyoruz ve iptal token'ı dinliyoruz.
				await rabbitMqService.Subscribe(
					acm.QueueName,
					ElasticSearch_AddOrUpdateAsync,
					ElasticSearch_DeleteAsync,
					stoppingToken);
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"{acm.AppName}: {nameof(ExecuteAsync)} sırasında hata oluştu.");
			}
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			Log.Information($"{acm.AppName} background servis durduruluyor.");
			await base.StopAsync(cancellationToken);
		}

		public async Task LogInit()
		{
			await rabbitMqService.Publish(acm.LogExchangeName, acm.LogRoutingKey, acm.LogQueueName, "", CancellationToken.None);
		}

		#region Article ElasticSearch islemleri

		public async Task<bool> ElasticSearch_AddOrUpdateAsync(string message)
		{
			try
			{
				Log.Information($"{acm.AppName}: {nameof(ElasticSearch_AddOrUpdateAsync)} çağrıldı. Mesaj: {message}");
				ArticleModel? article = JsonSerializer.Deserialize<ArticleModel>(message);
				if (article != null)
				{
					article.ElasticId = article.Id.ToString();
					await elasticSearchService.UpsertAsync(article.ElasticId, article);
					Log.Information($"{acm.AppName}: {nameof(ElasticSearch_AddOrUpdateAsync)} işlemi başarılı. Mesaj: {article}");
					return true;
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"{acm.AppName}: {nameof(ElasticSearch_AddOrUpdateAsync)} sırasında hata oluştu. Mesaj: {message}");
			}

			return false;
		}

		public async Task<bool> ElasticSearch_DeleteAsync(string message)
		{
			try
			{
				Log.Information($"{acm.AppName}: {nameof(ElasticSearch_DeleteAsync)} çağrıldı. Mesaj: {message}");
				if (string.IsNullOrEmpty(message))
					return true;

				List<int>? ids = JsonSerializer.Deserialize<List<int>>(message);
				if (ids is null)
					return true;

				foreach (var id in ids)
				{
					await elasticSearchService.DeleteByAsyncId(id);
					Log.Information($"{acm.AppName}: {nameof(ElasticSearch_DeleteAsync)} işlemi başarılı. Id: {id}");
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"{acm.AppName}: {nameof(ElasticSearch_DeleteAsync)} sırasında hata oluştu. Mesaj: {message}");
			}

			return true;
		}

		#endregion
	}
}
