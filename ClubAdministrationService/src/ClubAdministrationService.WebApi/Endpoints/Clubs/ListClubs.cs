using ClubAdministrationService.Application.Clubs.Queries.ListClubs;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Clubs;

public sealed class ListClubs : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("subscriptions/{subscriptionId:guid}/clubs",
			async (
				Guid subscriptionId,
				ISender sender,
				CancellationToken cancellationToken) =>
			{
				ListClubsQuery query = new(subscriptionId);

				Result<List<Club>> listClubsResult = await sender.Send(query, cancellationToken);

				IResult response = listClubsResult.Match(
					onSuccess: clubs => Results.Ok(clubs.ConvertAll(c => new ClubResponse(c.Id, c.Name))),
					onFailure: errors => ProblemDetailsMapper.Problem([errors.Error]));

				return response;
			})
		.WithTags(Tags.Clubs)
		.WithSummary("List clubs")
		.WithDescription("List clubs for a subscription")
		.Produces<ClubResponse>();
	}
}
