using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using PArticle.TagSubscriber.Model;
using Serilog;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PArticle.TagSubscriber
{
	public class TagService : BackgroundService
	{
		private readonly IRedisService redisService;
		private readonly IRabbitMqService rabbitMqService;
		private readonly AppConstantModel acm;

		public TagService(IRedisService redisService, IRabbitMqService rabbitMqService, IOptions<AppConstantModel> options)
		{
			this.redisService = redisService;
			this.rabbitMqService = rabbitMqService;
			this.acm = options.Value;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				Log.Information("{AppName} başlatıldı", acm.AppName);

				await rabbitMqService.Publish(acm.LogExchangeName, acm.LogRoutingKey, acm.LogQueueName, "", stoppingToken);

				await rabbitMqService.Subscribe(acm.QueueName, AddOrUpdateAsync, DeleteAsync, stoppingToken);

				// Servisin kapanmaması için sonsuz bekleme
				while (!stoppingToken.IsCancellationRequested)
				{
					await Task.Delay(1000, stoppingToken);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "{AppName}: ExecuteAsync sırasında hata oluştu", acm.AppName);
			}
		}

		public async Task<bool> AddOrUpdateAsync(string message)
		{
			try
			{
				Log.Information("{AppName}: {Method} çağrıldı. Mesaj: {Message}", acm.AppName, nameof(AddOrUpdateAsync), message);
				TagModel? tag = JsonSerializer.Deserialize<TagModel>(message);
				if (tag != null)
				{
					await redisService.SetStringAsync($"{acm.SlugPrefix}{tag.Slug}", tag.Id.ToString());
					await redisService.AddOrUpdateAsync(acm.HashName, tag.Id, tag);
					return true;
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "{AppName}: {Method} sırasında hata oluştu. Mesaj: {Message}", acm.AppName, nameof(AddOrUpdateAsync), message);
			}
			return false;
		}

		public async Task<bool> DeleteAsync(string message)
		{
			try
			{
				Log.Information("{AppName}: {Method} çağrıldı. Mesaj: {Message}", acm.AppName, nameof(DeleteAsync), message);
				if (string.IsNullOrWhiteSpace(message))
					return true;

				List<int>? ids = JsonSerializer.Deserialize<List<int>>(message);
				if (ids == null)
					return true;

				foreach (var id in ids)
				{
					TagModel? tag = await redisService.GetAsync<TagModel>(acm.HashName, id.ToString());
					if (tag == null)
						continue;

					await redisService.RemoveKeyAsync($"{acm.SlugPrefix}{tag.Slug}");
					await redisService.DeleteAsync(acm.HashName, id);
				}
				return true;
			}
			catch (Exception ex)
			{
				Log.Error(ex, "{AppName}: {Method} sırasında hata oluştu. Mesaj: {Message}", acm.AppName, nameof(DeleteAsync), message);
				return false;
			}
		}
	}
}
