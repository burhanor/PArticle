using Microsoft.Extensions.Options;
using PArticle.Application.Abstractions.Enums;
using PArticle.Application.Abstractions.Interfaces.RabbitMq;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PArticle.Infrastructure.RabbitMq
{
	

	public class RabbitMqService : IRabbitMqService
	{
		private readonly RabbitMqModel _options;
		private IConnection? _connection;
		private IChannel? _channel;
		private readonly SemaphoreSlim _lock = new(1, 1);
		private bool _connected;

		public RabbitMqService(IOptions<RabbitMqModel> options)
		{
			_options = options.Value;
		}

		public async Task Connect(CancellationToken cancellationToken)
		{
			if (_connected)
				return;

			await _lock.WaitAsync(cancellationToken);
			try
			{
				if (_connected)
					return;

				var factory = new ConnectionFactory
				{
					HostName = _options.HOSTNAME,
					Port = _options.PORT,
					UserName = _options.RABBITMQ_DEFAULT_USER,
					Password = _options.RABBITMQ_DEFAULT_PASS
				};

				_connection = await factory.CreateConnectionAsync(cancellationToken);
				_channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

				_connected = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"RabbitMQ connection error: {ex.Message}");
				throw; // yeniden fırlat ki yukarıdaki katman da bilsin
			}
			finally
			{
				_lock.Release();
			}
		}

		public async Task Publish(Exchanges exchange, RoutingTypes routingType, object message, CancellationToken cancellationToken)
		{
			string exchangeName = exchange.ToString();
			string routingKey = routingType.ToString();
			string queueName = exchange.ToString();

			if (!_connected || _channel is null)
			{
				await Connect(cancellationToken);
			}

			try
			{
				await _channel.ExchangeDeclareAsync(exchangeName.ToString(), ExchangeType.Direct, durable: true, autoDelete: false, arguments: null, cancellationToken: cancellationToken);

				if (!string.IsNullOrEmpty(queueName))
				{
					await _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);
					await _channel.QueueBindAsync(queueName, exchangeName, routingKey, arguments: null, cancellationToken: cancellationToken);
				}

				var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
				await _channel.BasicPublishAsync(exchangeName, routingKey, body, cancellationToken);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Publish error: {ex.Message}");
				throw;
			}
		}

		public void Subscribe(string queueName)
		{
			throw new NotImplementedException();
		}

		public async ValueTask DisposeAsync()
		{
			if (_channel is not null)
				await _channel.DisposeAsync();

			if (_connection is not null)
				await _connection.DisposeAsync();

			_lock.Dispose();
		}

	

	}

}
