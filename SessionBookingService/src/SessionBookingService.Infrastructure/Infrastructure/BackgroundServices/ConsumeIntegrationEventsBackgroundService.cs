// in future, you need to upgrade the RabbitMQ client - and change the various method calls to their new way of doing things.

#pragma warning disable S3261
namespace SessionBookingService.Infrastructure.Infrastructure.BackgroundServices;
#pragma warning restore S3261
/*
internal sealed class ConsumeIntegrationEventsBackgroundService : IHostedService
{
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ILogger<ConsumeIntegrationEventsBackgroundService> _logger;
	private readonly CancellationTokenSource _cts;
	private readonly IConnection _connection;
	private readonly IModel _channel;
	private readonly MessageBrokerSettings _messageBrokerSettings;

	public ConsumeIntegrationEventsBackgroundService(
		ILogger<ConsumeIntegrationEventsBackgroundService> logger,
		IServiceScopeFactory serviceScopeFactory,
		IOptions<MessageBrokerSettings> messageBrokerOptions)
	{
		_logger = logger;
		_cts = new CancellationTokenSource();
		_serviceScopeFactory = serviceScopeFactory;

		_messageBrokerSettings = messageBrokerOptions.Value;

		IConnectionFactory connectionFactory = new ConnectionFactory
		{
			HostName = _messageBrokerSettings.HostName,
			Port = _messageBrokerSettings.Port,
			UserName = _messageBrokerSettings.UserName,
			Password = _messageBrokerSettings.Password
		};

		_connection = connectionFactory.CreateConnection();

		_channel = _connection.CreateModel();

		_channel.ExchangeDeclare(_messageBrokerSettings.ExchangeName, ExchangeType.Fanout, durable: true);

		_channel.QueueDeclare(
			queue: _messageBrokerSettings.QueueName,
			durable: false,
			exclusive: false,
			autoDelete: false);

		_channel.QueueBind(
			_messageBrokerSettings.QueueName,
			_messageBrokerSettings.ExchangeName,
			routingKey: string.Empty);

		EventingBasicConsumer consumer = new(_channel);

		consumer.Received += PublishIntegrationEvent;

		_channel.BasicConsume(_messageBrokerSettings.QueueName, autoAck: false, consumer);
	}

	private async void PublishIntegrationEvent(object? sender, BasicDeliverEventArgs eventArgs)
	{
		if (_cts.IsCancellationRequested)
		{
#pragma warning disable CA1848
			_logger.LogInformation("Cancellation requested, not consuming integration event.");
#pragma warning restore CA1848
			return;
		}

		try
		{
#pragma warning disable CA1848
			_logger.LogInformation("Received integration event. Reading message from queue.");
#pragma warning restore CA1848

			using IServiceScope scope = _serviceScopeFactory.CreateScope();

			var body = eventArgs.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);

			IIntegrationEvent? integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(message);

			if (integrationEvent is null)
			{
				throw new JsonException(message: "The received message could not be deserialized into an integration event");
			}

#pragma warning disable CA1848
			_logger.LogInformation(
				"Received integration event of type: {IntegrationEventType}. Publishing event.",
				integrationEvent.GetType().Name);
#pragma warning restore CA1848

			var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
			await publisher.Publish(integrationEvent);

#pragma warning disable CA1848
			_logger.LogInformation("Integration event published in Gym Management service successfully. Sending ack to message broker.");
#pragma warning restore CA1848

			_channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
		}
		catch (Exception e)
		{
#pragma warning disable CA1848
			_logger.LogError(e, "Exception occurred while consuming integration event");
#pragma warning restore CA1848
		}
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
#pragma warning disable CA1848
		_logger.LogInformation("Starting integration event consumer background service.");
#pragma warning restore CA1848
		return Task.CompletedTask;
	}

	public async Task StopAsync(CancellationToken cancellationToken)
	{
		await _cts.CancelAsync();
		_cts.Dispose();
	}
} */
