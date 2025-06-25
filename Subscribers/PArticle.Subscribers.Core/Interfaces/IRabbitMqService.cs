using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Subscribers.Core.Interfaces
{
	public interface IRabbitMqService
	{
		Task Connect(CancellationToken cancellationToken);
		Task Publish(string exchangeName, string routingKey, string queueName, object message, CancellationToken cancellationToken);
		Task Subscribe(string queueName, Func<string, Task<bool>> createOrUpdateFunc, Func<string, Task<bool>> deleteFunc);
		ValueTask DisposeAsync();
	}
}
