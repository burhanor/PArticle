using Microsoft.Extensions.Options;
using PArticle.ArticleSubscriber.Model;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using Serilog;
using System.Text.Json;

namespace PArticle.ArticleSubscriber
{
	public class ArticleService(IRedisService redisService, IRabbitMqService rabbitMqService,IElasticSearchService<ArticleModel> elasticSearchService, IOptions<AppConstantModel> options)
	{
		public AppConstantModel acm = options.Value;
		public async Task Run()
		{
			try
			{
				Log.Information($"{acm.AppName} başlatıldı");
				var articleTask = ArticleElasticSearchSubscribe(CancellationToken.None);

				await Task.WhenAll(articleTask);
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"{acm.AppName}: ${nameof(Run)} sırasında hata oluştu. Kuyruk: {acm.QueueName}");
			}
		}

		public async Task LogInit()
		{
			await rabbitMqService.Publish(acm.LogExchangeName, acm.LogRoutingKey, acm.LogQueueName, "", CancellationToken.None);
		}
		#region Article ElasticSearch islemleri
		private async Task ArticleElasticSearchSubscribe(CancellationToken cancellationToken)
		{
			await rabbitMqService.Subscribe(acm.QueueName, ElasticSearch_AddOrUpdateAsync, ElasticSearch_DeleteAsync, cancellationToken);
		}

		public async Task<bool> ElasticSearch_AddOrUpdateAsync(string message)
		{
			try
			{
				Log.Information($"{acm.AppName}: {nameof(ElasticSearch_AddOrUpdateAsync)} çağrıldı. Mesaj: {message}");
				ArticleModel? article = JsonSerializer.Deserialize<ArticleModel>(message);
				if (article != null)
				{
					article.ElasticId=article.Id.ToString();
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
