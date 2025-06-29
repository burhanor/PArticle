using Microsoft.Extensions.Options;
using PArticle.LogSubscriber.Model;
using PArticle.Subscribers.Core.Interfaces;
using PArticle.Subscribers.Core.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PArticle.LogSubscriber
{
	public class LogService(IRedisService redisService, IRabbitMqService rabbitMqService, IElasticSearchService<ElasticLogModel> elasticSearchService, IOptions<AppConstantModel> options)
	{
		public AppConstantModel acm = options.Value;

		public async Task Run()
		{
			try
			{
				var articleTask = LogElasticSearchSubscribe(CancellationToken.None);

				await Task.WhenAll(articleTask);
			}
			catch (Exception ex)
			{
			}
		}

	

		private async Task LogElasticSearchSubscribe(CancellationToken cancellationToken)
		{
			await rabbitMqService.Subscribe(acm.QueueName, ElasticSearch_AddOrUpdateAsync, ElasticSearch_DeleteAsync, cancellationToken);
		}
		public async Task<bool> ElasticSearch_AddOrUpdateAsync(string message)
		{
			try
			{
				if (string.IsNullOrEmpty(message))
					return true;
				ElasticLogModel log = new(message);
				await elasticSearchService.UpsertAsync(log.ElasticId, log);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"ElasticSearch_AddOrUpdateAsync error: {ex.Message}");
			}

			return false;
		}

		public async Task<bool> ElasticSearch_DeleteAsync(string message)
		{

			return true;
		}
	}
}
