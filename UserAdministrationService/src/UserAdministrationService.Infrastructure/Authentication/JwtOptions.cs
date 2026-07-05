namespace UserAdministrationService.Infrastructure.Authentication;

public sealed record JwtOptions
{
	public const string Section = "JwtOptions";

	public string Audience { get; set; } = null!;
	public string Issuer { get; set; } = null!;
	public string Secret { get; set; } = null!;
	public int TokenExpirationInMinutes { get; set; }
}