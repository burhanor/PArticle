﻿using Microsoft.Extensions.Options;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using PArticle.TagSubscriber.Model;
using Serilog;
using System.Text.Json;

namespace PArticle.TagSubscriber
{
	public class TagService(IRedisService redisService, IRabbitMqService rabbitMqService, IOptions<AppConstantModel> options)
	{
		public AppConstantModel acm = options.Value;


		public async Task Run()
		{
			try
			{
				Log.Information( $"{acm.AppName} başlatıldı");
				await rabbitMqService.Subscribe(acm.QueueName, AddOrUpdateAsync, DeleteAsync,CancellationToken.None);
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


		public async Task<bool> AddOrUpdateAsync(string message)
		{
			try
			{
				Log.Information($"{acm.AppName}: {nameof(AddOrUpdateAsync)} çağrıldı. Mesaj: {message}");
				TagModel? tag = System.Text.Json.JsonSerializer.Deserialize<TagModel>(message);
				if (tag != null)
				{
					await redisService.SetStringAsync($"{acm.SlugPrefix}{tag.Slug}", tag.Id.ToString());
					await redisService.AddOrUpdateAsync(acm.HashName, tag.Id, tag);
					return true;
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"{acm.AppName}: {nameof(AddOrUpdateAsync)} sırasında hata oluştu. Mesaj: {message}");
			}
			
			return false;
		}

		public async Task<bool> DeleteAsync(string message)
		{

			try
			{
				Log.Information($"{acm.AppName}: {nameof(DeleteAsync)} çağrıldı. Mesaj: {message}");
				if (string.IsNullOrEmpty(message))
					return true;
				List<int>? ids = JsonSerializer.Deserialize<List<int>>(message);
				if (ids is null)
					return true;
				foreach (var id in ids)
				{
					TagModel? tag = await redisService.GetAsync<TagModel>(acm.HashName, id.ToString());
					if (tag is null)
						continue;
					await redisService.RemoveKeyAsync($"{acm.SlugPrefix}{tag.Slug}");
					await redisService.DeleteAsync(acm.HashName, id);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"{acm.AppName}: {nameof(DeleteAsync)} sırasında hata oluştu. Mesaj: {message}");
			}

			return true;
		}
	}
}
