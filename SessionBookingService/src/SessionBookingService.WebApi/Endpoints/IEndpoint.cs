namespace SessionBookingService.WebApi.Endpoints;

internal interface IEndpoint
{
	void MapEndpoint(IEndpointRouteBuilder app);
}
