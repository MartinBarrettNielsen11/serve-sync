using System.Text.Json.Serialization;

namespace ClubAdministrationService.Contracts.Subscriptions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubscriptionType
{
	Free,
	Starter,
	Pro
}