using Microsoft.Extensions.Options;
using PArticle.Subscribers.Core.Interfaces;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using PArticle.Subscribers.Core.Models;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using System.Threading.Channels;
using System.Threading;

namespace PArticle.Subscribers.Core.Concretes
{
	public class RabbitMqService:IRabbitMqService
	{
		private readonly RabbitMqModel _options;
		private IConnection? _connection;
		private IChannel? _channel;
		private readonly SemaphoreSlim _lock = new(1, 1);
		private bool _connected;
		BasicProperties basicProperties = new BasicProperties()
		{
			DeliveryMode =DeliveryModes.Persistent
		};
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
				Console.WriteLine($"RabbitMq bağlanılıyor: {_options.HostName}:{_options.Port}");

				var factory = new ConnectionFactory
				{
					HostName = _options.HostName,
					Port = _options.Port,
					UserName = _options.User,
					Password = _options.Password
				};

				_connection = await factory.CreateConnectionAsync(cancellationToken);
				_channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

				_connected = true;
				Console.WriteLine($"RabbitMq bağlanıldı: {_options.HostName}:{_options.Port}");

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

		public async Task Publish(string exchangeName, string routingKey,string queueName, object message, CancellationToken cancellationToken)
		{

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
				await _channel.BasicPublishAsync(
					exchange: exchangeName,
					routingKey: routingKey,
					mandatory: false,
					basicProperties: basicProperties,
					body: body,
					cancellationToken: cancellationToken
				);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Publish error: {ex.Message}");
				throw;
			}
		}

		public async Task Subscribe(string queueName, Func<string, Task<bool>> createOrUpdateFunc, Func<string, Task<bool>> deleteFunc,CancellationToken cancellationToken)
		{
			if (!_connected || _channel is null)
			{
				await Connect(CancellationToken.None);
			}
			try
			{

				if (_channel is null)
					throw new InvalidOperationException("Channel is not initialized.");
				await _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);

				var consumer = new AsyncEventingBasicConsumer(_channel);
				consumer.ReceivedAsync += async (model, ea) =>
				{
					var body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine($"Received message: {message}");
					string routingKey = ea.RoutingKey;

					var selectedFunc = ea.RoutingKey switch
					{
						"Created" => createOrUpdateFunc,
						"Updated" => createOrUpdateFunc,
						"Deleted" => deleteFunc,
						_ => null
					};
					if (selectedFunc is null)
						return;

					if (await selectedFunc(message))
					{
						// İşlem başarılıysa mesajı kuyruktan sil (ack)
						await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
						Console.WriteLine("Message processed and acknowledged.");
					}
					else
					{
						// İşlem başarısızsa mesajı geri bırak (nack, requeue = true)
						await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
						Console.WriteLine("Message processing failed. Message requeued.");
					}
					// Process the message here
					await Task.CompletedTask; // Simulate async processing
				};
				await _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
				Console.WriteLine($"Subscribed to queue: {queueName}");
				while (!cancellationToken.IsCancellationRequested)
				{
					await Task.Delay(1000, cancellationToken); 
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Subscribe error: {ex.Message}");
				
			}

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
