using ClubAdministrationService.Application.Clubs.Commands.AddInstructor;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.WebApi.Infrastructure;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Clubs;

public sealed class AddInstructor : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("subscriptions/{subscriptionId:guid}/clubs/{clubId:guid}/instructors",
					async (AddInstructorRequest request,
							Guid subscriptionId,
							Guid clubId,
							ISender sender,
							CancellationToken cancellationToken) =>
					{
						AddInstructorCommand command = new(subscriptionId, clubId, request.InstructorId);

						Result addTrainerResult = await sender.Send(command, cancellationToken);

						IResult result = addTrainerResult.Match(() => Results.Ok(clubId),
																err => ProblemDetailsMapper.Problem([err.Error]));

						return result;
					})
			.WithTags(Tags.Clubs)
			.WithSummary("Add instructor")
			.WithDescription("Add instructor for a subscription (and a club)");
		//.RequireAuthorization();
	}
}
