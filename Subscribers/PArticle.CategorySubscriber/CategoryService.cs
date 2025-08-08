using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PArticle.CategorySubscriber.Model;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using Serilog;
using System.Text.Json;

namespace PArticle.CategorySubscriber
{
	public class CategoryService(
		IRedisService redisService,
		IRabbitMqService rabbitMqService,
		IOptions<AppConstantModel> options) : BackgroundService
	{
		private readonly AppConstantModel acm = options.Value;

		public async Task<bool> AddOrUpdateAsync(string message)
		{
			try
			{
				Log.Information("{AppName}: {Method} çağrıldı. Mesaj: {Message}", acm.AppName, nameof(AddOrUpdateAsync), message);
				var category = JsonSerializer.Deserialize<CategoryModel>(message);
				if (category is not null)
				{
					await redisService.SetStringAsync($"{acm.SlugPrefix}{category.Slug}", category.Id.ToString());
					await redisService.AddOrUpdateAsync(acm.HashName, category.Id, category);
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

				var ids = JsonSerializer.Deserialize<List<int>>(message);
				if (ids is null)
					return true;

				foreach (var id in ids)
				{
					var category = await redisService.GetAsync<CategoryModel>(acm.HashName, id.ToString());
					if (category is null)
						continue;

					await redisService.RemoveKeyAsync($"{acm.SlugPrefix}{category.Slug}");
					await redisService.DeleteAsync(acm.HashName, id);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "{AppName}: {Method} sırasında hata oluştu. Mesaj: {Message}", acm.AppName, nameof(DeleteAsync), message);
			}
			return true;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{

			try
			{
				Log.Information($"{acm.AppName} başlatıldı.");
				await rabbitMqService.Publish(acm.LogExchangeName, acm.LogRoutingKey, acm.LogQueueName, "", stoppingToken);
				await rabbitMqService.Subscribe(acm.QueueName, AddOrUpdateAsync, DeleteAsync, stoppingToken);

				while (!stoppingToken.IsCancellationRequested)
				{
					await Task.Delay(1000, stoppingToken); // servis kapanmasın diye beklemede tut
				}
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, $"{acm.AppName}: ExecuteAsync sırasında kritik hata.");
			}
			
		}
	}
}
