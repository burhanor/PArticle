using PArticle.Subscribers.Core.Interfaces;
using PArticle.TagSubscriber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PArticle.TagSubscriber
{
	public class TagService(IRedisService redisService, IRabbitMqService rabbitMqService)
	{
		string queueName = "Tag";
		string hashName = "tags";
		public async Task Run()
		{
			await rabbitMqService.Subscribe(queueName, AddOrUpdateAsync, DeleteAsync);
		}


		public async Task<bool> AddOrUpdateAsync(string message)
		{
			TagModel? tag = System.Text.Json.JsonSerializer.Deserialize<TagModel>(message);
			if (tag!=null)
			{
				await redisService.AddOrUpdateAsync(hashName, tag.Id, tag);
				return true;
			}
			return false;
		}

		public async Task<bool> DeleteAsync(string message)
		{
			if (string.IsNullOrEmpty(message))
				return true;
			List<int>? ids = JsonSerializer.Deserialize<List<int>>(message);
			if (ids is null)
				return true;
			foreach (var id in ids)
			{
				await redisService.DeleteAsync(hashName, id);
			}
			return true;
		}
	}
}
