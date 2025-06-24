using PArticle.Application.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Abstractions.Interfaces.RabbitMq
{
	public interface IRabbitMqService: IAsyncDisposable
	{
		Task Connect(CancellationToken cancellationToken);

		Task Publish(Exchanges exchange, RoutingTypes routingType, object message, CancellationToken cancellationToken);

		void Subscribe(string queueName);

	}
}
