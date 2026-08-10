using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SessionBookingService.Infrastructure.Infrastructure.Settings;
using SharedKernel.IntegrationEvents;

namespace SessionBookingService.Infrastructure.Infrastructure.IntegrationEvents;

internal sealed class IntegrationEventsPublisher : IIntegrationEventsPublisher
{
	private readonly MessageBrokerSettings _messageBrokerSettings;
	private readonly IModel _channel;

	public IntegrationEventsPublisher(IOptions<MessageBrokerSettings> messageBrokerOptions)
	{
		_messageBrokerSettings = messageBrokerOptions.Value;
		ConnectionFactory connectionFactory = new()
		{
			HostName = _messageBrokerSettings.HostName,
			Port = _messageBrokerSettings.Port,
			UserName = _messageBrokerSettings.UserName,
			Password = _messageBrokerSettings.Password
		};

		IConnection connection = connectionFactory.CreateConnection();

		_channel = connection.CreateModel();
		_channel.ExchangeDeclare(
			_messageBrokerSettings.ExchangeName,
			ExchangeType.Fanout,
			durable: true);
	}

	public void PublishEvent(IIntegrationEvent integrationEvent)
	{
		var serializedIntegrationEvent = JsonSerializer.Serialize(integrationEvent);

		var body = Encoding.UTF8.GetBytes(serializedIntegrationEvent);

		_channel.BasicPublish(
			exchange: _messageBrokerSettings.ExchangeName,
			routingKey: string.Empty,
			body: body);
	}
}
