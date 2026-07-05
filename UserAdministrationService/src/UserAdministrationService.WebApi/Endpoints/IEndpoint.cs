namespace UserAdministrationService.WebApi.Endpoints;

internal interface IEndpoint
{
	void MapEndpoint(IEndpointRouteBuilder app);
}